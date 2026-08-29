using UFF.FichaAnestesica.Domain.Security;

namespace UFF.FichaAnestesica.Test.Security
{
    public class PasswordHasherTest
    {
        [Fact]
        public void Hash_Should_Not_Return_PlainText()
        {
            var hash = PasswordHasher.Hash("minhaSenha123");

            Assert.NotEqual("minhaSenha123", hash);
            Assert.StartsWith("v1.", hash);
        }

        [Fact]
        public void Hash_Should_Produce_Different_Output_For_Same_Password()
        {
            var hash1 = PasswordHasher.Hash("minhaSenha123");
            var hash2 = PasswordHasher.Hash("minhaSenha123");

            Assert.NotEqual(hash1, hash2); 
        }

        [Fact]
        public void Verify_Should_Return_True_For_Correct_Password_Against_Hash()
        {
            var hash = PasswordHasher.Hash("minhaSenha123");

            Assert.True(PasswordHasher.Verify("minhaSenha123", hash));
        }

        [Fact]
        public void Verify_Should_Return_False_For_Wrong_Password_Against_Hash()
        {
            var hash = PasswordHasher.Hash("minhaSenha123");

            Assert.False(PasswordHasher.Verify("outraSenha", hash));
        }

        [Fact]
        public void Verify_Should_Fall_Back_To_Plain_Text_Comparison_For_Legacy_Rows()
        {
            const string legacyPlainTextPassword = "admin123";

            Assert.True(PasswordHasher.Verify("admin123", legacyPlainTextPassword));
            Assert.False(PasswordHasher.Verify("wrong", legacyPlainTextPassword));
        }

        [Fact]
        public void Verify_Should_Return_False_For_Null_Or_Empty_Stored_Value()
        {
            Assert.False(PasswordHasher.Verify("anything", null));
            Assert.False(PasswordHasher.Verify("anything", ""));
        }

        [Fact]
        public void IsHashed_Should_Distinguish_Hashed_From_Legacy_Plain_Text()
        {
            var hash = PasswordHasher.Hash("minhaSenha123");

            Assert.True(PasswordHasher.IsHashed(hash));
            Assert.False(PasswordHasher.IsHashed("admin123"));
            Assert.False(PasswordHasher.IsHashed(null));
        }
    }
}
