using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Jatchley.Samples.Models
{
    public class ClaimViewModel
    {
        public ClaimViewModel(ClaimsPrincipal user)
        {
            Claims = GetClaimValueModels(user);
        }

        private IEnumerable<ClaimValue> GetClaimValueModels(ClaimsPrincipal user)
        {
            if(user == null || user.Claims == null)
            {
                return Enumerable.Empty<ClaimValue>();
            }

            return user
            .Claims
            .Select(x => new ClaimValue 
            {
                Value = x.Value,
                Type = x.Type
            });
        }

        public IEnumerable<ClaimValue> Claims {get; private set;}
    }

    public class ClaimValue 
    {
        public string Value {get; set;}
        public string Type {get; set;}
    }
}