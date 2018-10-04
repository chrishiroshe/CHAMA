using System;
using Courses.Email.Interface;

namespace Courses.Email.Providers
{
    public class SMTPProvider: IEmail
    {
        public bool SendMail(Email email)
        {
            //TODO: IMPLENT EMAIL
            return true;
        }
    }
}
