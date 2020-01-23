using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace CustomPolicyProvider
{
    // This class contains logic for determining whether ProjectRequirements in authorization
    // policies are satisfied or not
    internal class ProjectAuthorizationHandler : AuthorizationHandler<ProjectRequirement>
    {
        private readonly ILogger<ProjectAuthorizationHandler> _logger;

        public ProjectAuthorizationHandler(ILogger<ProjectAuthorizationHandler> logger)
        {
            _logger = logger;
        }

        // Check whether a given ProjectRequirement is satisfied or not for a particular context
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProjectRequirement requirement)
        {
            // Log as a warning so that it's very clear in sample output which authorization policies 
            // (and requirements/handlers) are in use
            _logger.LogWarning("Evaluating authorization requirement for ProjectName = {ProjectName}", requirement.ProjectName);

            // Check the first letter of user's name
            var firstLetterOfUserName = context.User?.Identity.Name?.FirstOrDefault();
            if (firstLetterOfUserName != null && 
                requirement.ProjectName.Equals(firstLetterOfUserName.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                _logger.LogInformation("Project name requirement ({ProjectName}) satisfied", requirement.ProjectName);
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation("Current user's name {UserName} does not match project name {ProjectName}",
                        context.User.Identity.Name,
                        requirement.ProjectName);
            }

            return Task.CompletedTask;
        }
    }
}
