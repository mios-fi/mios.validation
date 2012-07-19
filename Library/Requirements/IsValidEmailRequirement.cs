using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Mios.Validation.Requirements {
  public class IsValidEmailRequirement : AbstractRequirement<string> {
    public IsValidEmailRequirement() {
      Message = "{0} is not a valid email address";
    }

    public string Message { get; set; }

    public override IEnumerable<ValidationError> Check(string value) {
      if(value==null) yield break;
      try {
				new MailAddress(value);
        yield break;
			} catch(FormatException) {}
      yield return new ValidationError { 
        Message = String.Format(Message,value)
      };
    }
  }
}