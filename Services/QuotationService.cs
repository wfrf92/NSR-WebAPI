// Services/QuotationService.cs

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class QuotationService : IQuotationService
{
    private List<Quotation> _quotations;

    public QuotationService()
    {
        _quotations = GetQuotationFromJsonFile();
    }

    public List<Quotation> GetQuotationFromJsonFile()
    {
        var jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Json/quotations.json");

        if (!File.Exists(jsonFilePath))
        {
            // Handle the case when the file does not exist
            return new List<Quotation>();
        }

        var json = File.ReadAllText(jsonFilePath);
        var products = JsonConvert.DeserializeObject<List<Quotation>>(json);

        return products ?? new List<Quotation>();
    }

    public List<Quotation> GetAllQuotations()
    {
        return _quotations;
    }

    public Quotation GetQuotationById(int id)
    {
        return _quotations.Find(quotation => quotation.Id == id);
    }

    public void AddQuotation(Quotation newQuotation)
    {
        _quotations.Add(newQuotation);
    }

    public void UpdateQuotation(Quotation updatedQuotation)
    {
        var index = _quotations.FindIndex(quotation => quotation.Id == updatedQuotation.Id);
        if (index != -1)
        {
            _quotations[index] = updatedQuotation;
        }
    }

    public void DeleteQuotation(int id)
    {
        _quotations.RemoveAll(quotation => quotation.Id == id);
    }
}
