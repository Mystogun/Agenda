using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LawAgendaApi.Data;
using LawAgendaApi.Data.Entities;
using LawAgendaApi.Data.Queries.Search;
using LawAgendaApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace LawAgendaApi.Repositories
{
    //Todo: Recheck IsDeleted Conditions
    public class CaseRepo : ICaseRepo
    {
        private readonly DataContext _context;

        public CaseRepo(DataContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Case>> GetPublicCases(DateTime? created = null)
        {
            if (created == null)
            {
                var cases = await _context.Cases
                    .Include(c => c.Customer)
                    .Include(c => c.Type)
                    .OrderByDescending(c => c.CreatedAt)
                    .Take(30)
                    .Where(c => c.IsDeleted > 0)
                    .ToListAsync();

                return cases;
            }
            else
            {
                var cases = await _context.Cases
                    .Include(c => c.Customer)
                    .Include(c => c.Type)
                    .OrderBy(c => c.CreatedAt)
                    .Take(30)
                    .Where(c => c.IsDeleted > 0 && c.CreatedAt >= created)
                    .ToListAsync();

                return cases;
            }
        }

        public async Task<Case> GetCaseById(long caseId)
        {
            var caseById = await _context.Cases
                .Include(c => c.Customer)
                .Include(c => c.Type)
                .FirstOrDefaultAsync(c => c.IsDeleted > 0 && c.Id == caseId);

            return caseById;
        }


        public async Task<IEnumerable<Case>> Search(SearchQuery<CaseSearchQueryType> query,
            PriceOperationType operationType, bool singleFilter)
        {
            if (string.IsNullOrEmpty(query.Query) || string.IsNullOrWhiteSpace(query.Query))
            {
                return null;
            }

            var cases = new List<Case>();

            double price = -1;
            if (query.Query.IsValueDouble())
            {
                price = Convert.ToDouble(query.Query);
            }

            if (singleFilter)
            {
                switch (query.Filter)
                {
                    case CaseSearchQueryType.Number:
                        cases = await _context.Cases.Where(c => 
                                c.IsDeleted > 0 && EF.Functions.Like(c.Number, $"%{query.Query}%"))
                            .ToListAsync();
                        break;
                    case CaseSearchQueryType.Name:
                        cases = await _context.Cases.Where(c =>
                                c.IsDeleted <= 0 && EF.Functions.Like(c.Name, $"%{query.Query}%"))
                            .ToListAsync();
                        break;
                    case CaseSearchQueryType.StartingDate:
                        cases = await _context.Cases.Where(c =>
                                c.IsDeleted <= 0 && c.StartingDate >= DateTime.Parse(query.Query))
                            .ToListAsync();
                        break;
                    case CaseSearchQueryType.NextDate:
                        cases = await _context.Cases
                            .Where(c => c.IsDeleted <= 0 && c.NextDate >= DateTime.Parse(query.Query))
                            .ToListAsync();
                        break;
                    case CaseSearchQueryType.Closed:
                        cases = await _context.Cases.Where(c => c.IsDeleted <= 0 && c.IsClosed > 0).ToListAsync();
                        break;
                    case CaseSearchQueryType.Private:
                        cases = await _context.Cases.Where(c => c.IsDeleted <= 0 && c.IsPrivate > 0).ToListAsync();
                        break;
                    case CaseSearchQueryType.Price:
                        if (price <= -1)
                        {
                            cases = null;
                            break;
                        }

                        switch (operationType)
                        {
                            case PriceOperationType.Unknown:
                                cases = null;
                                break;
                            case PriceOperationType.Equal:
                                cases = await _context.Cases.Where(c => c.Price == price)
                                    .ToListAsync();
                                break;
                            case PriceOperationType.GreaterThan:
                                cases = await _context.Cases.Where(c => c.Price > price)
                                    .ToListAsync();
                                break;
                            case PriceOperationType.GreaterThanOrEqualTo:
                                cases = await _context.Cases.Where(c => c.Price >= price)
                                    .ToListAsync();
                                break;
                            case PriceOperationType.LessThan:
                                cases = await _context.Cases.Where(c => c.Price < price)
                                    .ToListAsync();
                                break;
                            case PriceOperationType.LessThanOrEqualTo:
                                cases = await _context.Cases.Where(c => c.Price <= price)
                                    .ToListAsync();
                                break;
                            default:
                                cases = null;
                                break;
                        }

                        break;
                    default:
                        cases = null;
                        break;
                }

                return cases;
            }


            cases = await _context.Cases.Where(c =>
                    c.IsDeleted <= 0 &&
                    EF.Functions.Like(c.Name, $"%{query.Query}%") ||
                    EF.Functions.Like(c.Name, $"%{query.Query}%")
                )
                .ToListAsync();

            return cases;
        }

        public async Task<SaveChangesToDbResult<Case>> CreateCase(Case newCase, User admin, IEnumerable<User> users)
        {
            var result = await _context.AddAsync(newCase);

            var isSaved = await _context.SaveChangesAsync();

            var createResult = new SaveChangesToDbResult<Case>();

            if (isSaved <= 0)
            {
                createResult.Message = "Could Not Save, Try Again Later";
                createResult.Entity = null;
                return createResult;
            }

            foreach (var user in users)
            {
                var timeline = new CaseTimeline
                {
                    Notes = $"The Lawyer {user.Name} Has Been Assigned to This Case By {admin.Name}.",
                    CaseId = result.Entity.Id,
                    UserId = user.Id
                };
                await _context.AddAsync(timeline);
            }

            isSaved = await _context.SaveChangesAsync();


            if (isSaved > 0)
            {
                createResult.Message = "Success";
                createResult.Entity = result.Entity;
                return createResult;
            }


            createResult.Message = "Could Not Save, Try Again Later";
            createResult.Entity = null;
            return createResult;
        }

        public async Task<SaveChangesToDbResult<CaseTimeline>> AddToCaseTimeline(CaseTimeline caseTimeline)
        {
            var result = await _context.AddAsync(caseTimeline);

            var isSaved = await _context.SaveChangesAsync();

            var addResult = new SaveChangesToDbResult<CaseTimeline>();

            if (isSaved > 0)
            {
                addResult.Message = "Success";
                addResult.Entity = result.Entity;

                return addResult;
            }

            addResult.Message = "Could Not Save, Try Again Later";
            addResult.Entity = null;
            return addResult;
        }

        public async Task<SaveChangesToDbResult<Case>> CloseCaseById(long id)
        {
            var caseFromDb = await GetCaseById(id);

            caseFromDb.UpdatedAt = DateTime.UtcNow.AddHours(3);
            caseFromDb.IsClosed = 1;

            var updateResult = _context.Update(caseFromDb);

            var isSaved = await _context.SaveChangesAsync();

            var result = new SaveChangesToDbResult<Case>();

            if (isSaved > 0)
            {
                result.Message = "Success";
                result.Entity = updateResult.Entity;

                return result;
            }

            result.Message = "Could Not Save, Try Again Later";
            result.Entity = null;
            return result;
        }

        public async Task<IEnumerable<CaseTimeline>> GetCaseTimelineByCaseId(long caseId)
        {
            var timeline = await _context.CaseTimelines
                .Include(c => c.User)
                .Include(c => c.User.Type)
                .Include(c => c.User.Avatar)
                .Include(c => c.File)
                .Where(c => c.IsDeleted <= 0 && c.CaseId == caseId).ToListAsync();

            return timeline;
        }

        public async Task<CaseTimeline> GetCaseTimelineById(long timelineId)
        {
            var timeline = await _context.CaseTimelines
                .Include(c => c.Case)
                .Include(c => c.Case.Type)
                .Include(c => c.Case.Customer)
                .Include(c => c.User)
                .Include(c => c.User.Type)
                .Include(c => c.File)
                .FirstOrDefaultAsync(c => c.IsDeleted <= 0 && c.Id == timelineId);
            return timeline;
        }

        public async Task<IEnumerable<Case>> GetCasesByType(short typeId)
        {
            var cases = await _context.Cases
                .Include(c => c.Customer)
                .Include(c => c.Type)
                .Where(c => c.TypeId == typeId)
                .ToListAsync();

            return cases;
        }

        public async Task<IEnumerable<Case>> GetCasesForUser(long userId, DateTime? created = null,
            DateTime? next = null)
        {
            var ids = from c in _context.CaseTimelines
                where c.UserId == userId
                group c by c.CaseId
                into g
                select new
                {
                    CaseId = g.Key,
                    Count = g.Count()
                };


            var cases = new List<Case>();
            var caseById = new Case();

            foreach (var id in ids)
            {
                if (created != null)
                {
                    caseById = await _context.Cases
                        .Include(c => c.Type)
                        .Include(c => c.Customer)
                        .FirstOrDefaultAsync(c => c.IsDeleted <= 0 && c.Id == id.CaseId && c.CreatedAt >= created);
                }
                else if (next != null)
                {
                    caseById = await _context.Cases
                        .Include(c => c.Type)
                        .Include(c => c.Customer)
                        .FirstOrDefaultAsync(c => c.IsDeleted <= 0 && c.Id == id.CaseId && c.NextDate >= next);
                }
                else
                {
                    caseById = await _context.Cases
                        .Include(c => c.Type)
                        .Include(c => c.Customer)
                        .FirstOrDefaultAsync(c => c.IsDeleted <= 0 && c.Id == id.CaseId);
                }

                cases.Add(caseById);
            }

            return cases;
        }

        public async Task<IEnumerable<Case>> GetAllCasesForAdmin(int page = 1, int pageSize = 30)
        {
            var cases = await _context.Cases
                .OrderByDescending(c => c.CreatedAt)
                .Skip(page * pageSize)
                .Take(pageSize)
                .Where(c => c.IsDeleted <= 0)
                .ToListAsync();

            return cases;
        }

        public Task<Case> UpdateCase(Case updatedCase, User admin)
        {
            throw new NotImplementedException();
        }

        public Task<CaseTimeline> RemoveLawyerFromCase(CaseTimeline updatedTimeline, User admin)
        {
            throw new NotImplementedException();
        }
    }
}