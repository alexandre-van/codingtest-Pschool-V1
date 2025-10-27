using Pschool.Client.Models;

namespace Pschool.Client.Services
{
    public class FormStateService
    {
        public StudentDTO? StudentBeingCreated { get; set; }
        public ParentDTO? ParentBeingCreated { get; set; }

        public void Clear()
        {
            StudentBeingCreated = null;
            ParentBeingCreated = null;
        }
    }
}