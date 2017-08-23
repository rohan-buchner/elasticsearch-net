﻿using System;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.DeleteJob
{
	public class DeleteJobApiTests : ApiIntegrationTestBase<XPackCluster, IDeleteJobResponse, IDeleteJobRequest, DeleteJobDescriptor, DeleteJobRequest>
	{
		public DeleteJobApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// TODO: create a job, to allow it to be deleted

		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteJob(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.DeleteJobAsync(CallIsolatedValue, f),
			request: (client, r) => client.DeleteJob(r),
			requestAsync: (client, r) => client.DeleteJobAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"_xpack/ml/anomaly_detectors/{CallIsolatedValue}/_close";

		protected override bool SupportsDeserialization => true;

		protected override DeleteJobDescriptor NewDescriptor() => new DeleteJobDescriptor(CallIsolatedValue);

		protected override object ExpectJson => null;

		protected override Func<DeleteJobDescriptor, IDeleteJobRequest> Fluent => f => f;

		protected override DeleteJobRequest Initializer =>
			new DeleteJobRequest(CallIsolatedValue);

		protected override void ExpectResponse(IDeleteJobResponse response)
		{
			response.Closed.Should().BeTrue();
		}
	}
}