// Interfaces/ISliderService.cs

using System.Collections.Generic;

public interface ISliderService
{
    List<Slider> GetAllSliders();
    Slider GetSliderById(int id);
    void AddSlider(Slider newSlider);
    void UpdateSlider(Slider updatedSlider);
    void DeleteSlider(int id);
}
