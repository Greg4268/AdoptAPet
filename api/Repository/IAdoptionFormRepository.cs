using api.Models;

namespace api.Repository
{
    public interface IAdoptionFormRepository
    {
        public List<AdoptionForm> GetAllAdoptionForms();
        public void SaveToDB(AdoptionForm form);
        public AdoptionForm GetAdoptionFormById(int formId);
        public void UpdateToDB(AdoptionForm form);
        public void DeleteAdoptionForm(AdoptionForm value);
    }        
}