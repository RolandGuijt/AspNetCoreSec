using System.Threading.Tasks;
using AspNetSecurity_m4_Shared.Models;
using Microsoft.AspNetCore.Authorization;

namespace AspNetSecurity_m4.Authorization
{
    public class ProposalApprovedAuthorizationHandler: 
        AuthorizationHandler<ProposalRequirement, ProposalModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            ProposalRequirement requirement, ProposalModel resource)
        {
            if (!requirement.MustBeApproved)
                if (resource.Approved)
                    context.Fail();

            if (requirement.MustBeApproved)
                if (!resource.Approved)
                    context.Fail();

            return Task.CompletedTask;
        }
    }
}
