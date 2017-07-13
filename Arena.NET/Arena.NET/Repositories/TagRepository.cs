using Arena.NET.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Arena.NET.Repositories
{
    public class TagRepository : Repository
    {

        public TagRepository(ArenaAPI api) : base(api)
        {

        }

        public async Task<ArenaPostResult> AddMember(int tagId, int personId)
        {
            Action = String.Format("profile/{0}/member/{1}/add", tagId, personId);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Action);
            String serializedContent = String.Empty;
            request.Content = new StringContent(serializedContent, Encoding.UTF8, "application/xml");

            return await ExecutePost(request);
        }
    }
}
