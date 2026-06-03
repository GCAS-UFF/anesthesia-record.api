using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Test.Entities
{
    public class DrugTest
    {
        [Fact]
        public void Create_ShouldSetPropertiesCorrectly()
        {
            // Arrange & Act
            var drug = Drug.Create("D001", "Dipirona", "mg");

            // Assert
            Assert.NotNull(drug);
            Assert.Equal("D001", drug.ExternalId);
            Assert.Equal("Dipirona", drug.Description);
            Assert.Equal("mg", drug.DefaultUnit);
            Assert.True(drug.Active);
            Assert.True((DateTime.UtcNow - drug.CreatedAt).TotalSeconds < 5);
            Assert.NotNull(drug.LastSyncAt);
            Assert.True((DateTime.UtcNow - drug.LastSyncAt!.Value).TotalSeconds < 5);
        }

        [Fact]
        public void Create_ShouldSetActiveToTrueAlways()
        {
            var drug = Drug.Create("X", "Y", "ml");
            Assert.True(drug.Active);
        }

        [Fact]
        public void Update_ShouldChangeDescription_WhenNotEmpty()
        {
            // Arrange
            var drug = Drug.Create("D002", "Paracetamol", "mg");

            // Act
            drug.Update("Paracetamol 500mg", "comprimido");

            // Assert
            Assert.Equal("Paracetamol 500mg", drug.Description);
            Assert.Equal("comprimido", drug.DefaultUnit);
            Assert.True(drug.Active); // Update reativa o medicamento
            Assert.True((DateTime.UtcNow - drug.LastSyncAt!.Value).TotalSeconds < 5);
        }

        [Fact]
        public void Update_ShouldKeepOldDescription_WhenNewDescriptionIsNull()
        {
            // Arrange
            var drug = Drug.Create("D003", "Ibuprofeno", "mg");

            // Act
            drug.Update(null, "ml");

            // Assert
            Assert.Equal("Ibuprofeno", drug.Description); // Mantém a descrição original
            Assert.Equal("ml", drug.DefaultUnit);
        }

        [Fact]
        public void Update_ShouldKeepOldDescription_WhenNewDescriptionIsWhitespace()
        {
            // Arrange
            var drug = Drug.Create("D004", "Cetoprofeno", "g");

            // Act
            drug.Update("   ", "ml");

            // Assert
            Assert.Equal("Cetoprofeno", drug.Description);
        }

        [Fact]
        public void Update_ShouldSetActiveToTrue()
        {
            // Arrange
            var drug = Drug.Create("D005", "Dipirona", "ml");
            drug.Disable(); // Desabilita primeiro
            Assert.False(drug.Active);

            // Act
            drug.Update("Dipirona Sódica", "ampola");

            // Assert
            Assert.True(drug.Active);
        }

        [Fact]
        public void Disable_ShouldSetActiveToFalseAndUpdateLastSync()
        {
            // Arrange
            var drug = Drug.Create("D006", "Morfinina", "ampola");
            var lastSyncBefore = drug.LastSyncAt;

            // Act
            drug.Disable();

            // Assert
            Assert.False(drug.Active);
            Assert.True(drug.LastSyncAt > lastSyncBefore);
        }

        [Fact]
        public void Disable_ShouldKeepOtherPropertiesUnchanged()
        {
            // Arrange
            var drug = Drug.Create("D007", "Adrenalina", "mg");
            var originalExternalId = drug.ExternalId;
            var originalDescription = drug.Description;
            var originalUnit = drug.DefaultUnit;

            // Act
            drug.Disable();

            // Assert
            Assert.Equal(originalExternalId, drug.ExternalId);
            Assert.Equal(originalDescription, drug.Description);
            Assert.Equal(originalUnit, drug.DefaultUnit);
        }
    }
}
