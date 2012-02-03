using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Mios.Validation;
using Mios.Validation.Extensions;
using Xunit;

namespace Tests {
	public struct Name {
		public string First { get; set; }
		public string Last { get; set; }
	}

	public class NameValidator : Validator<Name> {
		public static readonly NameValidator Strict = new NameValidator(t => {
			t.Require(c => c.First).NotEmpty().AtMost(128);
			t.Require(c => c.Last).NotEmpty().AtMost(128);
		});

		public NameValidator(Action<NameValidator> init) {
			init.Invoke(this);
		}
	}

	public struct Address {
		public string Street { get; set; }
		public string Postalcode { get; set; }
		public string City { get; set; }
	}

	public class AddressValidator : Validator<Address> {
		public AddressValidator() {
			Require(t => t.Street).NotEmpty();
			Require(t => t.Postalcode).NotEmpty();
			Require(t => t.City).NotEmpty();
		}
	}

	public enum Sex {
		None,
		Female,
		Male,
		Other
	}

	public class User {
		public User Mother { get; set; }
		public Name Name { get; set; }
		public int Age { get; set; }
		public Sex Sex { get; set; }
		public string Email { get; set; }
		public string Telephone { get; set; }
		public string Password { get; set; }
		public string PasswordConfirm { get; set; }
		public Address Address { get; set; }
	}

	public class PersonValidator : Validator<User> {
		public static readonly PersonValidator Strict = new PersonValidator(t => {
			t.Require(c => c.Mother).NotNull();
			t.Require(c => c.Sex).Reject(Sex.None);
			t.Require(c => c.Name).ValidatedBy(NameValidator.Strict);
			t.Require(c => c.Age).Gt(0).Lt(120);
			t.Require(c => c.Address).If(
				c => !String.IsNullOrEmpty(c.Street+c.Postalcode+c.City), 
				c => c.ValidatedBy(new AddressValidator())
			);
			t.Require(c => c.Email).NotEmpty().AtMost(128).Satisfies(c => {
				try {
					new MailAddress(c);
					return true;
				} catch {
					return false;
				}
			}, "Invalid email address");
			t.Require(c => c.Telephone).AtMost(16);
			t.Require(c => c.Password).NotEmpty().AtMost(128);
			t.Require(c => c).Satisfies(c => c.Password==c.PasswordConfirm, "Password and password confirmation must match");
		});

		public PersonValidator(Action<PersonValidator> init) {
			init.Invoke(this);
		}
	}

	public class IntegrationTests {
		[Fact]
		public void ShouldReturnExpectedKeys() {
			var errors = PersonValidator.Strict.Check(new User {
				Name = new Name { First = "Bob", Last = null },
				Mother = null,
				Email = "xxyyzz",
				Telephone = "12345678901234567890",
				Age = -10,
				Sex = Sex.None,
				Password = "secret",
				PasswordConfirm = "secert"
			}).ToArray();
			Assert.Equal(7, errors.Length);
			Assert.Contains(new ValidationError { Key = "Name.Last", Message = "A value is required" }, errors);
			Assert.Contains(new ValidationError { Key = "Mother", Message = "Required reference is missing" }, errors);
			Assert.Contains(new ValidationError { Key = "Email", Message = "Invalid email address" }, errors);
			Assert.Contains(new ValidationError { Key = "Telephone", Message = "Value is too long (20 characters while 16 are permitted)" }, errors);
			Assert.Contains(new ValidationError { Key = "Age", Message = "Value -10 is lower than the allowed minimum 0" }, errors);
			Assert.Contains(new ValidationError { Key = "Sex", Message = "Value None is not an allowed alternative" }, errors);
			Assert.Contains(new ValidationError { Key = "", Message = "Password and password confirmation must match" }, errors);
		}
		[Fact]
		public void ShouldReturnExpectedKeysWithPrefix() {
			var errors = PersonValidator.Strict.Check(new User {
				Name = new Name { First = "Bob", Last = null },
				Mother = null,
				Email = "xxyyzz",
				Telephone = "12345678901234567890",
				Age = -10,
				Sex = Sex.None,
				Password = "secret",
				PasswordConfirm = "secert"
			}, "form").ToArray();
			Assert.Equal(7, errors.Length);
			Assert.Contains(new ValidationError { Key = "form.Name.Last", Message = "A value is required" }, errors);
			Assert.Contains(new ValidationError { Key = "form.Mother", Message = "Required reference is missing" }, errors);
			Assert.Contains(new ValidationError { Key = "form.Email", Message = "Invalid email address" }, errors);
			Assert.Contains(new ValidationError { Key = "form.Telephone", Message = "Value is too long (20 characters while 16 are permitted)" }, errors);
			Assert.Contains(new ValidationError { Key = "form.Age", Message = "Value -10 is lower than the allowed minimum 0" }, errors);
			Assert.Contains(new ValidationError { Key = "form.Sex", Message = "Value None is not an allowed alternative" }, errors);
			Assert.Contains(new ValidationError { Key = "form", Message = "Password and password confirmation must match" }, errors);
		}
		[Fact]
		public void NestedValidators() {
			IEnumerable<ValidationError> errors;
			errors = PersonValidator.Strict.Check(new User {
				Address = new Address { Street = "Big street", City = "Bigtown" }
			}).ToArray();
			Assert.True(errors.Any(t=>t.Key.Equals("Address.Postalcode")));
			
			errors = PersonValidator.Strict.Check(new User {
				Address = new Address { Street = "", City = "" }
			}).ToArray();
			Assert.False(errors.Any(t=>t.Key.Equals("Address.Postalcode")));
		}
	}
}