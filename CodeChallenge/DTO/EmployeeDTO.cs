using System.Collections.Generic;
using System;

namespace CodeChallenge.DTO
{
    public class EmployeeDTO
    {
        private string firstName;
        private string lastName;
        private string position;
        private string department;
        private List<String> directReports;
        public String FirstName 
        { 
            get
            {
                return firstName;
            } 
            set
            {
                firstName = value;
            }
        }
        public String LastName 
        { 
            get
            {
                return lastName;
            } 
            set
            {
                lastName = value;
            } 
        }
        public String Position 
        { 
            get
            {
                return position;
            } 
            set
            {
                position = value;
            } 
        }
        public String Department 
        { 
            get
            {
                return department;
            } 
            set
            {
                department = value;
            } 
        }
        public List<String> DirectReports 
        { 
            get
            {
                return directReports;
            } 
            set
            {
                directReports = value;
            } 
        }
    }
}
