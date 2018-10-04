using System;
using System.Collections.Generic;
using System.Text;

namespace Courses.Email.Interface
{
    public interface IEmail
    {
        bool SendMail(Email email);
    }
}
