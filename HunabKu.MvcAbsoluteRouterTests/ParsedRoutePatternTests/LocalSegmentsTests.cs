﻿using System.Collections.Generic;
using HunabKu.MvcAbsoluteRouter;
using NUnit.Framework;
using SharpTestsEx;

namespace HunabKu.MvcAbsoluteRouterTests.ParsedRoutePatternTests
{
	public class LocalSegmentsTests
	{
		[Test]
		public void WhenPatternIsJustLocalThenAssignLocalPattern()
		{
			var parsed = ParsedRoutePattern.Parse("{area}");
			IEnumerable<string> actual = parsed.PathSegments;
			actual.Should().Have.SameSequenceAs("{area}");
		}

		[Test]
		public void WhenPatternContainsDomainThenAssignLocalPattern()
		{
			var parsed = ParsedRoutePattern.Parse("http://acme.com/{area}/Index");
			IEnumerable<string> actual = parsed.PathSegments;
			actual.Should().Have.SameSequenceAs("{area}","Index");
		}

		[Test]
		public void WhenPatternContainsQueryThenAssignLocalPattern()
		{
			var parsed = ParsedRoutePattern.Parse("{area}/{controller}?a=5&b=6");
			IEnumerable<string> actual = parsed.PathSegments;
			actual.Should().Have.SameSequenceAs("{area}","{controller}");
		}

		[Test]
		public void WhenPatternContainsDomainAndQueryThenAssignLocalPattern()
		{
			var parsed = ParsedRoutePattern.Parse("http://acme.com/{area}?a=5&b=6");
			IEnumerable<string> actual = parsed.PathSegments;
			actual.Should().Have.SameSequenceAs("{area}");
		}

		[Test]
		public void WhenPatternIsSchemaAndDomainThenEmptyLocalPattern()
		{
			var parsed = ParsedRoutePattern.Parse("http://{company}.com/");
			IEnumerable<string> actual = parsed.PathSegments;
			actual.Should().Be.Empty();
		}

		[Test]
		public void WhenPatternIsSchemaAndDomainWithoutEndSlashThenEmptyLocalPattern()
		{
			var parsed = ParsedRoutePattern.Parse("http://{company}.com");
			IEnumerable<string> actual = parsed.PathSegments;
			actual.Should().Be.Empty();
		}

		[Test]
		public void WhenPatternIsJustDomainThenEmptyLocalPattern()
		{
			var parsed = ParsedRoutePattern.Parse("{company}.com");
			IEnumerable<string> actual = parsed.PathSegments;
			actual.Should().Be.Empty();
		}

		[Test]
		public void WhenPatternEndWithSlashThenDoesNotIncludeItInSegments()
		{
			var parsed = ParsedRoutePattern.Parse("{controller}/Index/");
			IEnumerable<string> actual = parsed.PathSegments;
			actual.Should().Have.SameSequenceAs("{controller}", "Index");
		}

		[Test]
		public void WhenPatternIsJustSlashThenDoesNotIncludeItInSegments()
		{
			var parsed = ParsedRoutePattern.Parse("/");
			IEnumerable<string> actual = parsed.PathSegments;
			actual.Should().Be.Empty();
		}

		[Test]
		public void WhenPatternIsJustDomainWithSlashThenEmptyLocalPattern()
		{
			var parsed = ParsedRoutePattern.Parse("{company}.com/");
			IEnumerable<string> actual = parsed.PathSegments;
			actual.Should().Be.Empty();
		}
	}
}