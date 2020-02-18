namespace StudentsFromHell.Lib.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }
        public string Name { get; set; }

        public Subject(int subjectid, string name)
        {
            this.SubjectID = subjectid;
            this.Name = name;
        }
    }
}
