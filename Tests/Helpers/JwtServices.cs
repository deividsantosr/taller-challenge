using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using ProductAPI.Helpers;

namespace Tests.Helpers
{
    public class JwtServiceTests
    {
        [Fact]
        public void GenerateToken_ReturnsValidJwtToken_WithUsernameClaim()
        {
            // Arrange
            var inMemorySettings = new Dictionary<string, string>
        {
            {"Jwt:Key", "supersecretkey_supersecretkey_1234567890!"},
            {"Jwt:Issuer", "TestIssuer"}
        };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var jwtService = new JwtService(configuration);
            string username = "testuser";

            // Act
            var tokenString = jwtService.GenerateToken(username);

            // Assert
            Assert.False(string.IsNullOrEmpty(tokenString));

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenString);

            Assert.Equal("TestIssuer", token.Issuer);
            Assert.Contains(token.Claims, c => c.Type == System.Security.Claims.ClaimTypes.Name && c.Value == username);
            Assert.True(token.ValidTo > DateTime.UtcNow);
        }
    }
}