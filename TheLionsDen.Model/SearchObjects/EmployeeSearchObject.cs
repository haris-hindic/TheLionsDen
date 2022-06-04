namespace TheLionsDen.Model.SearchObjects
{
    public class EmployeeSearchObject : BaseSearchObject
    {
        public string Name { get; set; }
        public int JobTypeId { get; set; }
        public int FacilityId { get; set; }
        public bool IncludeJobType { get; set; }
        public bool IncludeFacility { get; set; }
    }
}
