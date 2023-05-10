using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Crud.WebApi.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public DateTime DateOfBirth { get; set; } // Try using DateOnly
    }

    public  class DateTimeConverter : System.Text.Json.Serialization.JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            //Debug.Assert(typeToConvert == typeof(DateTime));
            if(DateTime.TryParse(reader.GetString(), out DateTime tempDate))
            {
                return tempDate;
            }

            return DateTime.Now;                 
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd")); // Bug resolved: value.ToUniversalTime -> value
        }
    }
}
