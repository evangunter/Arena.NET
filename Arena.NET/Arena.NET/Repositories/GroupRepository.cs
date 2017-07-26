using Arena.NET.Helpers;
using Arena.NET.Objects;
using Arena.NET.Objects.APIObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Arena.NET.Repositories
{
    public class GroupRepository : Repository
    {

        public GroupRepository(ArenaAPI api) : base(api)
        {

        }

        public async Task<ArenaPostResult> InsertOrUpdate(int groupId, int personId, GroupMember groupMember)
        {
            Action = String.Format("group/{0}/member/{1}/add", groupId, personId);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Action);
            String serializedContent = groupMember.Serialize();
            request.Content = new StringContent(serializedContent, Encoding.UTF8, "application/xml");

            return await ExecutePost(request);
        }

        public async Task<List<GroupMember>> Get(int groupId)
        {
            Action = String.Format("json/group/{0}/member/list", groupId);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Action);

            GroupMemberFromGetCollection groupMembers = await ExecuteGet<GroupMemberFromGetCollection>(request);

            //map to person object for consitency
            List<GroupMember> members = new List<GroupMember>();
            groupMembers.Members.ForEach(delegate (GroupMemberFromGet member)
            {
                members.Add(new GroupMember(member));
            });

            return members;
        }


    } 
}

