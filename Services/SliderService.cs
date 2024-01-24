// Services/SliderService.cs

using System.Collections.Generic;
using Newtonsoft.Json;

public class SliderService : ISliderService
{
    private List<Slider> _sliders;

    public SliderService()
    {
        _sliders = GetSliderFromJsonFile();
    }

    public List<Slider> GetSliderFromJsonFile()
    {
        var jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Json/sliders.json");

        if (!File.Exists(jsonFilePath))
        {
            // Handle the case when the file does not exist
            return new List<Slider>();
        }

        var json = File.ReadAllText(jsonFilePath);
        var products = JsonConvert.DeserializeObject<List<Slider>>(json);

        return products ?? new List<Slider>();
    }

    public List<Slider> GetAllSliders()
    {
        return _sliders;
    }

    public Slider GetSliderById(int id)
    {
        return _sliders.Find(slider => slider.Id == id);
    }

    public void AddSlider(Slider newSlider)
    {
        newSlider.Active = true;
        newSlider.Id = _sliders.Count + 1;
        _sliders.Add(newSlider);

          // Serialize the updated products list to JSON
        string json = JsonConvert.SerializeObject(_sliders);

        // Specify the path to your JSON file
        string jfilePath = "Json/sliders.json";

        // Write the JSON data to the file
         File.WriteAllTextAsync(jfilePath, json);

    }

    public void UpdateSlider(Slider updatedSlider)
    {
        var index = _sliders.FindIndex(slider => slider.Id == updatedSlider.Id);
        if (index != -1)
        {
            _sliders[index] = updatedSlider;
        }
    }

    public void DeleteSlider(int id)
    {
        _sliders.RemoveAll(slider => slider.Id == id);
    }
}
