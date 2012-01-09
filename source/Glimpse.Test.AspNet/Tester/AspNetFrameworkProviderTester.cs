using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using Glimpse.AspNet;
using Moq;

namespace Glimpse.Test.AspNet.Tester
{
    public class AspNetFrameworkProviderTester : AspNetFrameworkProvider
    {
        public Mock<HttpContextBase> HttpContextMock { get; set; }
        public Mock<HttpApplicationStateBase> HttpApplicationStateMock { get; set; }
        public Mock<HttpResponseBase> HttpResponseMock { get; set; }
        public NameValueCollection HeaderCollection { get; set; }
    

        private AspNetFrameworkProviderTester()
        {
            HttpResponseMock = new Mock<HttpResponseBase>();
            HeaderCollection = new NameValueCollection();
            HttpResponseMock.Setup(r => r.Headers).Returns(HeaderCollection);

            HttpApplicationStateMock = new Mock<HttpApplicationStateBase>();
            HttpApplicationStateMock.Setup(st => st.Get("testKey")).Returns("testValue");

            HttpContextMock = new Mock<HttpContextBase>();
            HttpContextMock.Setup(ctx => ctx.Response).Returns(HttpResponseMock.Object);
            HttpContextMock.Setup(c => c.Application).Returns(HttpApplicationStateMock.Object);
            HttpContextMock.Setup(ctx => ctx.Items)
                .Returns(new Dictionary<object, object>
                             {
                                 {typeof (string).FullName, "TestString"},
                                 {typeof (int).FullName, 5}
                             });

            Context = HttpContextMock.Object;
        }

        public static AspNetFrameworkProviderTester Create()
        {
            return new AspNetFrameworkProviderTester();
        }
    }
}