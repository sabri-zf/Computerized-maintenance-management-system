using Computerized_maintenance_Logic_layer.Module.User_Management.Extensions;
using computrized_maintenance_Data_Access;


namespace Computerized_maintenance_Logic_layer_Test
{
    public class UserService_Test
    {


        [Fact]
        public void Reset_Password_IfUserIdGreatThanZeroAndNewPasswordIsNotNull_ShouldReturnTure()
        {

        }

        [Fact]
        public void Reset_Password_IfPassedCondidtionTrySetThemAndCheckIsSucceed_ShouldReturnTureHasBeenResetedPassword()
        {

        }

        [Fact]

        public void GetPersonIdOfUser_SetUserIdAndCheckIfLessThanOne_ShouldReturnNull()
        {
            // Arrange  // Act // Assert
            //Assert.Null(UserService.GetPersonIdOfUser(0));
        }


        [Fact]

        public void GetPersonIdOfUser_SetUserIdAndCheckIfGetAutoincrementId_ShouldReturnTrue()
        {
            // Arrange 

            // Act 

            // Assert
        }


        [Fact]

        public void Verfiy_User_Login_IfUsernameAndPasswordIsEmpty_ShouldReturnFalse()
        {
            //Assert.False(UserService.Verfiy_User_Login("", ""));
        }

        [Fact]
        public void Verfiy_User_Login_IfUsernameAndPasswordIsNull_ShouldReturnFalse()
        {
            //Assert.False(UserService.Verfiy_User_Login(null, null));
        }

        [Fact]
        public void Verfiy_User_Login_IfUsernameAndPasswordIsValid_ShouldReturnTrue()
        {
            // Arrange 

            // Act 

            // Assert        
        }
    }
}
