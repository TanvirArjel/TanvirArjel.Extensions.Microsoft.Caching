using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace AspNetCore5._0.Models
{
    public class Employee
    {
        private Employee(int id, string name)
        {
            Id = id;
            Name = name;
        }

        // This is for serialization.
        private Employee()
        {
        }

        [JsonInclude]
        public int Id { get; private set; }

        [JsonInclude]
        public string Name { get; private set; }

        public static Employee Create(int id, string name)
        {
            return new Employee(id, name);
        }
    }
}
