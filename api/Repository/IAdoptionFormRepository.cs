using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Repository;

namespace api.Repository
{
    public interface IAdoptionFormRepository
    {
        public List<AdoptionForm> GetAllAdoptionForms();
        public void SaveToDB();
        public AdoptionForm GetAdoptionFormById(int formId);
        public void UpdateToDB();
        public void DeleteAdoptionForm(AdoptionForm value);
    }        
}