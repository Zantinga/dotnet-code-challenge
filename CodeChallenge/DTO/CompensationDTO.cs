using System;

namespace CodeChallenge.DTO
{
    public class CompensationDTO
    {
        private string employeeId;
        private decimal salary;
        private DateTime effectiveDate;
        public String EmployeeId
        {
            get
            {
                return employeeId;
            }
            set 
            { 
                employeeId = value; 
            }
        }
        public decimal Salary 
        { 
            get
            {
                return salary;
            }
            set 
            { 
                salary = value; 
            }
        }
        public DateTime EffectiveDate 
        { 
            get
            {
                return effectiveDate;
            }
            set
            {
                effectiveDate = value;
            }
        }
    }
}
