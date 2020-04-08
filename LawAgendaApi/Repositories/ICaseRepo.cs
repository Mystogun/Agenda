using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LawAgendaApi.Data;
using LawAgendaApi.Data.Entities;
using LawAgendaApi.Data.Queries.Search;

namespace LawAgendaApi.Repositories
{
    public interface ICaseRepo
    {
        Task<IEnumerable<Case>> GetPublicCases(DateTime? created = null);
        Task<Case> GetCaseById(long caseId);
        Task<IEnumerable<Case>> Search(SearchQuery<CaseSearchQueryType> query, PriceOperationType operationType, bool singleFilter);
        Task<SaveChangesToDbResult<Case>> CreateCase(Case newCase, User admin, IEnumerable<User> users);
        Task<SaveChangesToDbResult<CaseTimeline>> AddToCaseTimeline(CaseTimeline caseTimeline);
        Task<SaveChangesToDbResult<Case>> CloseCaseById(long id);
        Task<IEnumerable<CaseTimeline>> GetCaseTimelineByCaseId(long caseId);
        Task<CaseTimeline> GetCaseTimelineById(long timelineId);
        Task<IEnumerable<Case>> GetCasesByType(short typeId);

        Task<IEnumerable<Case>> GetCasesForUser(long userId, DateTime? created = null, DateTime? next = null);
        
        Task<IEnumerable<Case>> GetAllCasesForAdmin(int page, int pageSize);

        Task<Case> UpdateCase(Case updatedCase, User admin);
        Task<CaseTimeline> RemoveLawyerFromCase(CaseTimeline updatedTimeline, User admin);
    }
}