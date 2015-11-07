namespace ReadModels
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    //[Table("EmployeeView")]
    public class EmployeeView
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
