namespace MartenProject;

public class DataRecord
{
    public string Key { get; set; }
    public Person Data { get; set; }
}
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string Occupation { get; set; }
    public string MaritalStatus { get; set; }
}
