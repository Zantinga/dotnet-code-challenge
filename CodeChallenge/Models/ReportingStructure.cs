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
                employee = value;
            }
        }
        public int NumberOfReports { 
            get
            {
                return numberOfReports;
            }
            set 
            {
                numberOfReports = value;
                //TODO: VALIDATE IF NUMBEROFREPORTS IS CORRECT!!!
            }
        }
    }
}
