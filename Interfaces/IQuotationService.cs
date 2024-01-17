// Interfaces/IQuotationService.cs

using System;
using System.Collections.Generic;

public interface IQuotationService
{
    List<Quotation> GetAllQuotations();
    Quotation GetQuotationById(int id);
    void AddQuotation(Quotation newQuotation);
    void UpdateQuotation(Quotation updatedQuotation);
    void DeleteQuotation(int id);
}
