namespace CodeChallenge.Models
{
    public class ReportingStructure
    {
        // Using Properties instead of variables to better implement encapsulation

        private Employee employee;
        private int numberOfReports;

        public Employee Employee { 
            get
            {
                return employee;
            }
            set
            {
                //TODO: implement any necessary validation
                    //fields are filled?
                employee = value;
            }
        }
        public int NumberOfReports { 
            get
            {
                return numberOfReports;
            }
        }

        private void calculateNumberOfReports()
        {
            if(employee.DirectReports != null)
            {
                numberOfReports += employee.DirectReports.Count;
                //TODO: FINISH BUILDING THIS WHILE WATCHING TIDO!!!

            }
        }
    }
}
