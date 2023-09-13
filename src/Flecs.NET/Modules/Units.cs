using System.Diagnostics.CodeAnalysis;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public static unsafe partial class Ecs
    {
        /// <summary>
        ///     Units module.
        /// </summary>
        [SuppressMessage("Usage", "CA1815")]
        public struct Units : IFlecsModule
        {
            /// <summary>
            ///     Initializes units module.
            /// </summary>
            /// <param name="world"></param>
            public readonly void InitModule(ref World world)
            {
                FlecsUnitsImport(world);

                world.Module<Units>();

                world.Entity<Prefixes>("global::flecs.units.prefixes");

                world.Entity<Yocto>("global::flecs.units.prefixes.Yocto");
                world.Entity<Zepto>("global::flecs.units.prefixes.Zepto");
                world.Entity<Atto>("global::flecs.units.prefixes.Atto");
                world.Entity<Femto>("global::flecs.units.prefixes.Femto");
                world.Entity<Pico>("global::flecs.units.prefixes.Pico");
                world.Entity<Nano>("global::flecs.units.prefixes.Nano");
                world.Entity<Micro>("global::flecs.units.prefixes.Micro");
                world.Entity<Milli>("global::flecs.units.prefixes.Milli");
                world.Entity<Centi>("global::flecs.units.prefixes.Centi");
                world.Entity<Deci>("global::flecs.units.prefixes.Deci");
                world.Entity<Deca>("global::flecs.units.prefixes.Deca");
                world.Entity<Hecto>("global::flecs.units.prefixes.Hecto");
                world.Entity<Kilo>("global::flecs.units.prefixes.Kilo");
                world.Entity<Mega>("global::flecs.units.prefixes.Mega");
                world.Entity<Giga>("global::flecs.units.prefixes.Giga");
                world.Entity<Tera>("global::flecs.units.prefixes.Tera");
                world.Entity<Peta>("global::flecs.units.prefixes.Peta");
                world.Entity<Exa>("global::flecs.units.prefixes.Exa");
                world.Entity<Zetta>("global::flecs.units.prefixes.Zetta");
                world.Entity<Yotta>("global::flecs.units.prefixes.Yotta");
                world.Entity<Kibi>("global::flecs.units.prefixes.Kibi");
                world.Entity<Mebi>("global::flecs.units.prefixes.Mebi");
                world.Entity<Gibi>("global::flecs.units.prefixes.Gibi");
                world.Entity<Tebi>("global::flecs.units.prefixes.Tebi");
                world.Entity<Pebi>("global::flecs.units.prefixes.Pebi");
                world.Entity<Exbi>("global::flecs.units.prefixes.Exbi");
                world.Entity<Zebi>("global::flecs.units.prefixes.Zebi");
                world.Entity<Yobi>("global::flecs.units.prefixes.Yobi");

                world.Entity<Duration>("global::flecs.units.Duration");
                world.Entity<Time>("global::flecs.units.Time");
                world.Entity<Mass>("global::flecs.units.Mass");
                world.Entity<Force>("global::flecs.units.Force");
                world.Entity<ElectricCurrent>("global::flecs.units.ElectricCurrent");
                world.Entity<Amount>("global::flecs.units.Amount");
                world.Entity<LuminousIntensity>("global::flecs.units.LuminousIntensity");
                world.Entity<Length>("global::flecs.units.Length");
                world.Entity<Pressure>("global::flecs.units.Pressure");
                world.Entity<Speed>("global::flecs.units.Speed");
                world.Entity<Temperature>("global::flecs.units.Temperature");
                world.Entity<Data>("global::flecs.units.Data");
                world.Entity<DataRate>("global::flecs.units.DataRate");
                world.Entity<Angle>("global::flecs.units.Angle");
                world.Entity<Frequency>("global::flecs.units.Frequency");
                world.Entity<Uri>("global::flecs.units.Uri");

                world.Entity<Durations.PicoSeconds>("global::flecs.units.Duration.PicoSeconds");
                world.Entity<Durations.NanoSeconds>("global::flecs.units.Duration.NanoSeconds");
                world.Entity<Durations.MicroSeconds>("global::flecs.units.Duration.MicroSeconds");
                world.Entity<Durations.MilliSeconds>("global::flecs.units.Duration.MilliSeconds");
                world.Entity<Durations.Seconds>("global::flecs.units.Duration.Seconds");
                world.Entity<Durations.Minutes>("global::flecs.units.Duration.Minutes");
                world.Entity<Durations.Hours>("global::flecs.units.Duration.Hours");
                world.Entity<Durations.Days>("global::flecs.units.Duration.Days");

                world.Entity<Times.CalenderDate>("global::flecs.units.Time.Date");

                world.Entity<Masses.Grams>("global::flecs.units.Mass.Grams");
                world.Entity<Masses.KiloGrams>("global::flecs.units.Mass.KiloGrams");

                world.Entity<ElectricCurrents.Ampere>("global::flecs.units.ElectricCurrent.Ampere");

                world.Entity<Amounts.Mole>("global::flecs.units.Amount.Mole");

                world.Entity<LuminousIntensities.Candela>("global::flecs.units.LuminousIntensity.Candela");

                world.Entity<Forces.Newton>("global::flecs.units.Force.Newton");

                world.Entity<Lengths.Meters>("global::flecs.units.Length.Meters");
                world.Entity<Lengths.PicoMeters>("global::flecs.units.Length.PicoMeters");
                world.Entity<Lengths.NanoMeters>("global::flecs.units.Length.NanoMeters");
                world.Entity<Lengths.MicroMeters>("global::flecs.units.Length.MicroMeters");
                world.Entity<Lengths.MilliMeters>("global::flecs.units.Length.MilliMeters");
                world.Entity<Lengths.CentiMeters>("global::flecs.units.Length.CentiMeters");
                world.Entity<Lengths.KiloMeters>("global::flecs.units.Length.KiloMeters");
                world.Entity<Lengths.Miles>("global::flecs.units.Length.Miles");
                world.Entity<Lengths.Pixels>("global::flecs.units.Length.Pixels");

                world.Entity<Pressures.Pascal>("global::flecs.units.Pressure.Pascal");
                world.Entity<Pressures.Bar>("global::flecs.units.Pressure.Bar");

                world.Entity<Speeds.MetersPerSecond>("global::flecs.units.Speed.MetersPerSecond");
                world.Entity<Speeds.KiloMetersPerSecond>("global::flecs.units.Speed.KiloMetersPerSecond");
                world.Entity<Speeds.KiloMetersPerHour>("global::flecs.units.Speed.KiloMetersPerHour");
                world.Entity<Speeds.MilesPerHour>("global::flecs.units.Speed.MilesPerHour");

                world.Entity<Temperatures.Kelvin>("global::flecs.units.Temperature.Kelvin");
                world.Entity<Temperatures.Celsius>("global::flecs.units.Temperature.Celsius");
                world.Entity<Temperatures.Fahrenheit>("global::flecs.units.Temperature.Fahrenheit");

                world.Entity<Datas.Bits>("global::flecs.units.Data.Bits");
                world.Entity<Datas.KiloBits>("global::flecs.units.Data.KiloBits");
                world.Entity<Datas.MegaBits>("global::flecs.units.Data.MegaBits");
                world.Entity<Datas.GigaBits>("global::flecs.units.Data.GigaBits");
                world.Entity<Datas.Bytes>("global::flecs.units.Data.Bytes");
                world.Entity<Datas.KiloBytes>("global::flecs.units.Data.KiloBytes");
                world.Entity<Datas.MegaBytes>("global::flecs.units.Data.MegaBytes");
                world.Entity<Datas.GigaBytes>("global::flecs.units.Data.GigaBytes");
                world.Entity<Datas.KibiBytes>("global::flecs.units.Data.KibiBytes");
                world.Entity<Datas.MebiBytes>("global::flecs.units.Data.MebiBytes");
                world.Entity<Datas.GibiBytes>("global::flecs.units.Data.GibiBytes");

                world.Entity<DataRates.BitsPerSecond>("global::flecs.units.DataRate.BitsPerSecond");
                world.Entity<DataRates.KiloBitsPerSecond>("global::flecs.units.DataRate.KiloBitsPerSecond");
                world.Entity<DataRates.MegaBitsPerSecond>("global::flecs.units.DataRate.MegaBitsPerSecond");
                world.Entity<DataRates.GigaBitsPerSecond>("global::flecs.units.DataRate.GigaBitsPerSecond");
                world.Entity<DataRates.BytesPerSecond>("global::flecs.units.DataRate.BytesPerSecond");
                world.Entity<DataRates.KiloBytesPerSecond>("global::flecs.units.DataRate.KiloBytesPerSecond");
                world.Entity<DataRates.MegaBytesPerSecond>("global::flecs.units.DataRate.MegaBytesPerSecond");
                world.Entity<DataRates.GigaBytesPerSecond>("global::flecs.units.DataRate.GigaBytesPerSecond");

                world.Entity<Frequencies.Hertz>("global::flecs.units.Frequency.Hertz");
                world.Entity<Frequencies.KiloHertz>("global::flecs.units.Frequency.KiloHertz");
                world.Entity<Frequencies.MegaHertz>("global::flecs.units.Frequency.MegaHertz");
                world.Entity<Frequencies.GigaHertz>("global::flecs.units.Frequency.GigaHertz");

                world.Entity<Uris.Hyperlink>("global::flecs.units.Uri.Hyperlink");
                world.Entity<Uris.Image>("global::flecs.units.Uri.Image");
                world.Entity<Uris.File>("global::flecs.units.Uri.File");

                world.Entity<Angles.Radians>("global::flecs.units.Angle.Radians");
                world.Entity<Angles.Degrees>("global::flecs.units.Angle.Degrees");

                world.Entity<Percentage>("global::flecs.units.Percentage");

                world.Entity<Bel>("global::flecs.units.Bel");
                world.Entity<DeciBel>("global::flecs.units.DeciBel");
            }

            /// <summary>
            ///     Prefixes.
            /// </summary>
            public struct Prefixes
            {
            }

            /// <summary>
            ///     Yocto unit.
            /// </summary>
            public struct Yocto
            {
            }

            /// <summary>
            ///     Zepto unit.
            /// </summary>
            public struct Zepto
            {
            }

            /// <summary>
            ///     Atto unit.
            /// </summary>
            public struct Atto
            {
            }

            /// <summary>
            ///     Femto unit.
            /// </summary>
            public struct Femto
            {
            }

            /// <summary>
            ///     Pico unit.
            /// </summary>
            public struct Pico
            {
            }

            /// <summary>
            ///     Nano unit.
            /// </summary>
            public struct Nano
            {
            }

            /// <summary>
            ///     Micro unit.
            /// </summary>
            public struct Micro
            {
            }

            /// <summary>
            ///     Milli unit.
            /// </summary>
            public struct Milli
            {
            }

            /// <summary>
            ///     Centi unit.
            /// </summary>
            public struct Centi
            {
            }

            /// <summary>
            ///     Deci unit.
            /// </summary>
            public struct Deci
            {
            }

            /// <summary>
            ///     Deca unit.
            /// </summary>
            public struct Deca
            {
            }

            /// <summary>
            ///     Hecto unit.
            /// </summary>
            public struct Hecto
            {
            }

            /// <summary>
            ///     Kilo unit.
            /// </summary>
            public struct Kilo
            {
            }

            /// <summary>
            ///     Mega unit.
            /// </summary>
            public struct Mega
            {
            }

            /// <summary>
            ///     Giga unit.
            /// </summary>
            public struct Giga
            {
            }

            /// <summary>
            ///     Tera unit.
            /// </summary>
            public struct Tera
            {
            }

            /// <summary>
            ///     Peta unit.
            /// </summary>
            public struct Peta
            {
            }

            /// <summary>
            ///     Exa unit.
            /// </summary>
            public struct Exa
            {
            }

            /// <summary>
            ///     Zetta unit.
            /// </summary>
            public struct Zetta
            {
            }

            /// <summary>
            ///     Yotta unit.
            /// </summary>
            public struct Yotta
            {
            }

            /// <summary>
            ///     Kibi unit.
            /// </summary>
            public struct Kibi
            {
            }

            /// <summary>
            ///     Mebi unit.
            /// </summary>
            public struct Mebi
            {
            }

            /// <summary>
            ///     Gibi unit.
            /// </summary>
            public struct Gibi
            {
            }

            /// <summary>
            ///     Tebi unit.
            /// </summary>
            public struct Tebi
            {
            }

            /// <summary>
            ///     Pebi unit.
            /// </summary>
            public struct Pebi
            {
            }

            /// <summary>
            ///     Exbi unit.
            /// </summary>
            public struct Exbi
            {
            }

            /// <summary>
            ///     Zebi unit.
            /// </summary>
            public struct Zebi
            {
            }

            /// <summary>
            ///     Yobi unit.
            /// </summary>
            public struct Yobi
            {
            }

            /// <summary>
            ///     Duration unit.
            /// </summary>
            public struct Duration
            {
            }

            /// <summary>
            ///     Time unit.
            /// </summary>
            public struct Time
            {
            }

            /// <summary>
            ///     Mass unit.
            /// </summary>
            public struct Mass
            {
            }

            /// <summary>
            ///     Electric current unit.
            /// </summary>
            public struct ElectricCurrent
            {
            }

            /// <summary>
            ///     Luminous intensity unit.
            /// </summary>
            public struct LuminousIntensity
            {
            }

            /// <summary>
            ///     Force unit.
            /// </summary>
            public struct Force
            {
            }

            /// <summary>
            ///     Amount unit.
            /// </summary>
            public struct Amount
            {
            }

            /// <summary>
            ///     Length unit.
            /// </summary>
            public struct Length
            {
            }

            /// <summary>
            ///     Pressure unit.
            /// </summary>
            public struct Pressure
            {
            }

            /// <summary>
            ///     Speed unit.
            /// </summary>
            public struct Speed
            {
            }

            /// <summary>
            ///     Temperature unit.
            /// </summary>
            public struct Temperature
            {
            }

            /// <summary>
            ///     Data unit.
            /// </summary>
            public struct Data
            {
            }

            /// <summary>
            ///     Data rate unit.
            /// </summary>
            public struct DataRate
            {
            }

            /// <summary>
            ///     Angle unit.
            /// </summary>
            public struct Angle
            {
            }

            /// <summary>
            ///     Frequency unit.
            /// </summary>
            public struct Frequency
            {
            }

            /// <summary>
            ///     Uri unit.
            /// </summary>
            public struct Uri
            {
            }

            /// <summary>
            ///     Durations.
            /// </summary>
            public struct Durations
            {
                /// <summary>
                ///     Picoseconds unit.
                /// </summary>
                public struct PicoSeconds
                {
                }

                /// <summary>
                ///     Nanoseconds unit.
                /// </summary>
                public struct NanoSeconds
                {
                }

                /// <summary>
                ///     Microseconds unit.
                /// </summary>
                public struct MicroSeconds
                {
                }

                /// <summary>
                ///     Milliseconds unit.
                /// </summary>
                public struct MilliSeconds
                {
                }

                /// <summary>
                ///     Seconds unit.
                /// </summary>
                public struct Seconds
                {
                }

                /// <summary>
                ///     Minutes unit.
                /// </summary>
                public struct Minutes
                {
                }

                /// <summary>
                ///     Hours unit.
                /// </summary>
                public struct Hours
                {
                }

                /// <summary>
                ///     Days unit.
                /// </summary>
                public struct Days
                {
                }
            }

            /// <summary>
            ///     Angles.
            /// </summary>
            public struct Angles
            {
                /// <summary>
                ///     Radians unit.
                /// </summary>
                public struct Radians
                {
                }

                /// <summary>
                ///     Degrees unit.
                /// </summary>
                public struct Degrees
                {
                }
            }

            /// <summary>
            ///     Times.
            /// </summary>
            public struct Times
            {
                /// <summary>
                ///     Date unit.
                /// </summary>
                public struct CalenderDate
                {
                }
            }

            /// <summary>
            ///     Masses.
            /// </summary>
            public struct Masses
            {
                /// <summary>
                ///     Grams unit.
                /// </summary>
                public struct Grams
                {
                }

                /// <summary>
                ///     Kilograms unit.
                /// </summary>
                public struct KiloGrams
                {
                }
            }

            /// <summary>
            ///     Electric currents.
            /// </summary>
            public struct ElectricCurrents
            {
                /// <summary>
                ///     Ampere unit.
                /// </summary>
                public struct Ampere
                {
                }
            }

            /// <summary>
            ///     Amounts.
            /// </summary>
            public struct Amounts
            {
                /// <summary>
                ///     Mole unit.
                /// </summary>
                public struct Mole
                {
                }
            }

            /// <summary>
            ///     Luminous intensities.
            /// </summary>
            public struct LuminousIntensities
            {
                /// <summary>
                ///     Candela unit.
                /// </summary>
                public struct Candela
                {
                }
            }

            /// <summary>
            ///     Forces.
            /// </summary>
            public struct Forces
            {
                /// <summary>
                ///     Newton unit.
                /// </summary>
                public struct Newton
                {
                }
            }

            /// <summary>
            ///     Lengths.
            /// </summary>
            public struct Lengths
            {
                /// <summary>
                ///     Meters unit.
                /// </summary>
                public struct Meters
                {
                }

                /// <summary>
                ///     Picometers unit.
                /// </summary>
                public struct PicoMeters
                {
                }

                /// <summary>
                ///     Nanometers unit.
                /// </summary>
                public struct NanoMeters
                {
                }

                /// <summary>
                ///     Micrometers unit.
                /// </summary>
                public struct MicroMeters
                {
                }

                /// <summary>
                ///     Millimeters unit.
                /// </summary>
                public struct MilliMeters
                {
                }

                /// <summary>
                ///     Centimeters unit.
                /// </summary>
                public struct CentiMeters
                {
                }

                /// <summary>
                ///     Kilometers unit.
                /// </summary>
                public struct KiloMeters
                {
                }

                /// <summary>
                ///     Miles unit.
                /// </summary>
                public struct Miles
                {
                }

                /// <summary>
                ///     Pixels unit.
                /// </summary>
                public struct Pixels
                {
                }
            }

            /// <summary>
            ///     Pressures.
            /// </summary>
            public struct Pressures
            {
                /// <summary>
                ///     Pascal unit.
                /// </summary>
                public struct Pascal
                {
                }

                /// <summary>
                ///     Bar unit.
                /// </summary>
                public struct Bar
                {
                }
            }

            /// <summary>
            ///     Speeds.
            /// </summary>
            public struct Speeds
            {
                /// <summary>
                ///     Meters per second unit.
                /// </summary>
                public struct MetersPerSecond
                {
                }

                /// <summary>
                ///     Kilometers per second unit.
                /// </summary>
                public struct KiloMetersPerSecond
                {
                }

                /// <summary>
                ///     Kilometers per hour unit.
                /// </summary>
                public struct KiloMetersPerHour
                {
                }

                /// <summary>
                ///     Miles per hour unit.
                /// </summary>
                public struct MilesPerHour
                {
                }
            }

            /// <summary>
            ///     Temperatures.
            /// </summary>
            public struct Temperatures
            {
                /// <summary>
                ///     Kelvin unit.
                /// </summary>
                public struct Kelvin
                {
                }

                /// <summary>
                ///     Celsius unit.
                /// </summary>
                public struct Celsius
                {
                }

                /// <summary>
                ///     Fahrenheit unit.
                /// </summary>
                public struct Fahrenheit
                {
                }
            }

            /// <summary>
            ///     Datas.
            /// </summary>
            public struct Datas
            {
                /// <summary>
                ///     Bits unit.
                /// </summary>
                public struct Bits
                {
                }

                /// <summary>
                ///     Kilobits unit.
                /// </summary>
                public struct KiloBits
                {
                }

                /// <summary>
                ///     Megabits unit.
                /// </summary>
                public struct MegaBits
                {
                }

                /// <summary>
                ///     Gigabits unit.
                /// </summary>
                public struct GigaBits
                {
                }

                /// <summary>
                ///     Bytes unit.
                /// </summary>
                public struct Bytes
                {
                }

                /// <summary>
                ///     Kilobytes unit.
                /// </summary>
                public struct KiloBytes
                {
                }

                /// <summary>
                ///     Megabytes unit.
                /// </summary>
                public struct MegaBytes
                {
                }

                /// <summary>
                ///     Gigabytes unit.
                /// </summary>
                public struct GigaBytes
                {
                }

                /// <summary>
                ///     Kibibytes unit.
                /// </summary>
                public struct KibiBytes
                {
                }

                /// <summary>
                ///     Mebibytes unit.
                /// </summary>
                public struct MebiBytes
                {
                }

                /// <summary>
                ///     Gibibytes unit.
                /// </summary>
                public struct GibiBytes
                {
                }
            }

            /// <summary>
            ///     Data rates.
            /// </summary>
            public struct DataRates
            {
                /// <summary>
                ///     Bits per second unit.
                /// </summary>
                public struct BitsPerSecond
                {
                }

                /// <summary>
                ///     Kilobits per second unit.
                /// </summary>
                public struct KiloBitsPerSecond
                {
                }

                /// <summary>
                ///     Megabits per second unit.
                /// </summary>
                public struct MegaBitsPerSecond
                {
                }

                /// <summary>
                ///     Gigabits per second unit.
                /// </summary>
                public struct GigaBitsPerSecond
                {
                }

                /// <summary>
                ///     Bytes per second unit.
                /// </summary>
                public struct BytesPerSecond
                {
                }

                /// <summary>
                ///     Kilobytes per second unit.
                /// </summary>
                public struct KiloBytesPerSecond
                {
                }

                /// <summary>
                ///     Megabytes per second unit.
                /// </summary>
                public struct MegaBytesPerSecond
                {
                }

                /// <summary>
                ///     Gigabytes per second unit.
                /// </summary>
                public struct GigaBytesPerSecond
                {
                }
            }

            /// <summary>
            ///     Frequencies,
            /// </summary>
            public struct Frequencies
            {
                /// <summary>
                ///     Hertz unit.
                /// </summary>
                public struct Hertz
                {
                }

                /// <summary>
                ///     Kilohertz unit.
                /// </summary>
                public struct KiloHertz
                {
                }

                /// <summary>
                ///     Megahertz unit.
                /// </summary>
                public struct MegaHertz
                {
                }

                /// <summary>
                ///     Gigahertz unit.
                /// </summary>
                public struct GigaHertz
                {
                }
            }

            /// <summary>
            ///     Uris.
            /// </summary>
            public struct Uris
            {
                /// <summary>
                ///     Hyper link unit.
                /// </summary>
                public struct Hyperlink
                {
                }

                /// <summary>
                ///     Image unit.
                /// </summary>
                public struct Image
                {
                }

                /// <summary>
                ///     File unit.
                /// </summary>
                public struct File
                {
                }
            }


            /// <summary>
            ///     Percentage unit.
            /// </summary>
            public struct Percentage
            {
            }

            /// <summary>
            ///     Bel unit.
            /// </summary>
            public struct Bel
            {
            }

            /// <summary>
            ///     Decibel unit.
            /// </summary>
            public struct DeciBel
            {
            }
        }
    }
}
