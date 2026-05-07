using Amazon.S3;
using Amazon.S3.Model;

namespace EventAlbumApp.Services.Implementation
{
    public class R2UrlService
    {
        private readonly IAmazonS3 _s3;
        private readonly IConfiguration _config;

        public R2UrlService(IAmazonS3 s3, IConfiguration config)
        {
            _s3 = s3;
            _config = config;
        }

        public string GetSignedUrl(string key, int expiresInMinutes = 60)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _config["S3:Bucket"],
                Key = key,
                Expires = DateTime.UtcNow.AddMinutes(expiresInMinutes),
                Verb = HttpVerb.GET
            };

            return _s3.GetPreSignedURL(request);
        }
    }
}
