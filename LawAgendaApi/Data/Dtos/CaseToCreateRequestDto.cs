using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace LawAgendaApi.Data.Dtos
{
    public class CaseToCreateRequestDto
    {
        
        [ModelBinder(Name = "admin_id")]public long AdminId { get; set; }
        [ModelBinder(Name = "number")]public string Number { get; set; }
        [ModelBinder(Name = "name")]public string Name { get; set; }
        [ModelBinder(Name = "starting_date")]public DateTime? StartingDate { get; set; }
        [ModelBinder(Name = "next_date")]public DateTime? NextDate { get; set; }
        [ModelBinder(Name = "customer_id")]public long? CustomerId { get; set; }
        [ModelBinder(Name = "type_id")]public short? TypeId { get; set; }
        [ModelBinder(Name = "is_private")]public byte? IsPrivate { get; set; }
        [ModelBinder(Name = "price")]public double? Price { get; set; }
        [ModelBinder(Name = "lawyers_ids")]public IEnumerable<long>? LawyersIds { get; set; }
    }
}