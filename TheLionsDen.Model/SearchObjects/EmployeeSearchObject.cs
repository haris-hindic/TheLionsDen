namespace TheLionsDen.Model.SearchObjects
{
    public class EmployeeSearchObject : BaseSearchObject
    {
        public string Name { get; set; }
        public string JobType { get; set; }
        public string Status { get; set; }
        public int FacilityId { get; set; }
    }
}
