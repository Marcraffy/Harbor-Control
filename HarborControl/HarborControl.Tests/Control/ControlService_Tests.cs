using HarborControl.Core.Clock;
using HarborControl.Core.Control;
using HarborControl.Core.Exceptions;
using HarborControl.Interfaces.Enums;
using HarborControl.Interfaces.Services;
using HarborControl.Tests.MockWeatherService;
using Shouldly;
using System.Linq;
using Xunit;

namespace HarborControl.Tests.Control
{
    public class ControlService_Tests : BaseTest
    {
        private readonly IControlService controlService;

        public ControlService_Tests()
        {
            Resolve<IClockService, ClockService>();
            Resolve<IWeatherService, WeatherService>();
            controlService = Resolve<IControlService, ControlService>();
        }

        [Fact]
        public void VesselArrived_WithNewVessel_AddsVessel()
        {
            //Assign
            var name = "TestBoat";

            //Act
            controlService.VesselArrived(name, Location.Harbor, VesselType.Cargoship);

            //Assert
            var testboat = controlService.VesselsAtHarbor.FirstOrDefault(vessel => vessel.Name == name);

            testboat.ShouldNotBeNull();
            testboat.Name.ShouldBe(name);
            testboat.Location.ShouldBe(Location.Harbor);
            testboat.Direction.ShouldBe(Direction.ToPerimeter);
        }

        [Fact]
        public void VesselArrived_WithSameVessel_ThrowsException()
        {
            //Assign
            var name = "TestBoat";
            controlService.VesselArrived(name, Location.Harbor, VesselType.Cargoship);

            //Act and Assert
            Should.Throw(() =>
                {
                    controlService.VesselArrived(name, Location.Harbor, VesselType.Cargoship);
                },
                typeof(ControlException)
            );
        }

        [Fact]
        public void VesselArrived_WithTransitVessel_ThrowsException()
        {
            //Assign
            var name = "TestBoat";

            //Act and Assert
            Should.Throw(() =>
                {
                    controlService.VesselArrived(name, Location.Transit, VesselType.Cargoship);
                },
                typeof(ControlException)
            );
        }

        [Fact]
        public void VesselDeparted_WithNoVessels_ThrowsException()
        {
            //Assign
            var name = "TestBoat";

            //Act and Assert
            Should.Throw(() =>
                {
                    controlService.VesselDeptarted(name);
                },
                typeof(ControlException)
            );
        }

        [Fact]
        public void VesselDeparted_WithVessels_ShouldRemoveFromQueue()
        {
            //Assign
            var name = "TestBoat";
            controlService.VesselArrived(name, Location.Harbor, VesselType.Cargoship);

            //Act
            controlService.VesselDeptarted(name);

            //Assert
            controlService.VesselsAtHarbor.Any(vessel => vessel.Name == name).ShouldBeFalse();
            controlService.VesselsAtPerimeter.Any(vessel => vessel.Name == name).ShouldBeFalse();
            controlService.VesselInTransit.ShouldBeNull();
        }
    }
}