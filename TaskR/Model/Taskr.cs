namespace TaskR.Model
{
    /// <summary>
    /// Model class for the tasks that are 
    /// created, read, updated and deleted.
    /// </summary>
    public class Taskr
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public string? Description { get; set; }

    }
}
