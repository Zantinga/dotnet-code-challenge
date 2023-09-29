using System;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        private String compensationId;
        private Employee employee;
        private decimal salary;
        private DateTime effectiveDate;

        public String CompensationId
        {
            get
            {
                return compensationId;
            }
            set
            {
                compensationId = value;
            }
        }
        public Employee Employee 
        { 
            get 
            { 
                return employee; 
            } 
            set 
            { 
                employee = value;
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
