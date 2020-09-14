using System.Net;
using System.Text.Json;
using AgoraIO.Media;
using AgoraTokenServer.Models;
using AgoraTokenServer.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AgoraTokenServer.Contollers
{
    [ApiController]
    [Route("[controller]")]
    public class AccessTokenController : ControllerBase
    {
        private readonly AppSettings appSettings;

        public AccessTokenController(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        [HttpPost]
        public ActionResult<AuthenticateResponse> index(AuthenticateRequest request)
        {
            if (string.IsNullOrEmpty(appSettings.AppID) || string.IsNullOrEmpty(appSettings.AppCertificate))
            {
                return new StatusCodeResult((int)HttpStatusCode.PreconditionFailed);
            }

            var uid = request.uid.ValueKind == JsonValueKind.Number ? request.uid.GetUInt64().ToString() : request.uid.GetString();
            var tokenBuilder = new AccessToken(appSettings.AppID, appSettings.AppCertificate, request.channel, uid);
            tokenBuilder.addPrivilege(Privileges.kJoinChannel, request.expiredTs);
            tokenBuilder.addPrivilege(Privileges.kPublishAudioStream, request.expiredTs);
            tokenBuilder.addPrivilege(Privileges.kPublishVideoStream, request.expiredTs);
            tokenBuilder.addPrivilege(Privileges.kPublishDataStream, request.expiredTs);
            tokenBuilder.addPrivilege(Privileges.kRtmLogin, request.expiredTs);
            return Ok(new AuthenticateResponse
            {
                channel = request.channel,
                uid = request.uid,
                token = tokenBuilder.build()
            });
        }
    }
}