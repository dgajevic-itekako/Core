// Copyright 2004-2010 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Castle.Components.Validator.Tests.ValidatorTests
{
	using System.Collections.Generic;
	using System.Globalization;
	using System.Threading;
	using NUnit.Framework;

	[TestFixture]
	public class CollectionNotEmptyValidatorTestCase
	{
		private CollectionNotEmptyValidator validator;
		private TestTarget target;

		[SetUp]
		public void Init()
		{
			Thread.CurrentThread.CurrentCulture =
				Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");

			validator = new CollectionNotEmptyValidator();
			validator.Initialize(new CachedValidationRegistry(), typeof(TestTarget).GetProperty("TargetField"));
			target = new TestTarget();
		}

		[Test]
		public void NullAndEmptyCollections()
		{
			Assert.IsFalse(validator.IsValid(target, null));
			Assert.IsFalse(validator.IsValid(target, new List<string>()));
		}

		[Test]
		public void NonEmptyCollection()
		{
			Assert.IsTrue(validator.IsValid(target, "1,2,3".Split(',')));
		}

		public class TestTarget
		{
			private IList<string> targetField = new List<string>();

			public IList<string> TargetField
			{
				get { return targetField; }
				set { targetField = value; }
			}
		}
	}
}