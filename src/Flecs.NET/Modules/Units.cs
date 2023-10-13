using System;
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
        private struct Units : IEquatable<Units>, IFlecsModule
        {
            /// <summary>
            ///     Initializes units module.
            /// </summary>
            /// <param name="world"></param>
            public readonly void InitModule(ref World world)
            {
                FlecsUnitsImport(world);

                world.Module<Units>();

                world.Entity<Prefixes>("::flecs.units.prefixes");

                world.Entity<Yocto>("::flecs.units.prefixes.Yocto");
                world.Entity<Zepto>("::flecs.units.prefixes.Zepto");
                world.Entity<Atto>("::flecs.units.prefixes.Atto");
                world.Entity<Femto>("::flecs.units.prefixes.Femto");
                world.Entity<Pico>("::flecs.units.prefixes.Pico");
                world.Entity<Nano>("::flecs.units.prefixes.Nano");
                world.Entity<Micro>("::flecs.units.prefixes.Micro");
                world.Entity<Milli>("::flecs.units.prefixes.Milli");
                world.Entity<Centi>("::flecs.units.prefixes.Centi");
                world.Entity<Deci>("::flecs.units.prefixes.Deci");
                world.Entity<Deca>("::flecs.units.prefixes.Deca");
                world.Entity<Hecto>("::flecs.units.prefixes.Hecto");
                world.Entity<Kilo>("::flecs.units.prefixes.Kilo");
                world.Entity<Mega>("::flecs.units.prefixes.Mega");
                world.Entity<Giga>("::flecs.units.prefixes.Giga");
                world.Entity<Tera>("::flecs.units.prefixes.Tera");
                world.Entity<Peta>("::flecs.units.prefixes.Peta");
                world.Entity<Exa>("::flecs.units.prefixes.Exa");
                world.Entity<Zetta>("::flecs.units.prefixes.Zetta");
                world.Entity<Yotta>("::flecs.units.prefixes.Yotta");
                world.Entity<Kibi>("::flecs.units.prefixes.Kibi");
                world.Entity<Mebi>("::flecs.units.prefixes.Mebi");
                world.Entity<Gibi>("::flecs.units.prefixes.Gibi");
                world.Entity<Tebi>("::flecs.units.prefixes.Tebi");
                world.Entity<Pebi>("::flecs.units.prefixes.Pebi");
                world.Entity<Exbi>("::flecs.units.prefixes.Exbi");
                world.Entity<Zebi>("::flecs.units.prefixes.Zebi");
                world.Entity<Yobi>("::flecs.units.prefixes.Yobi");

                world.Entity<Duration>("::flecs.units.Duration");
                world.Entity<Time>("::flecs.units.Time");
                world.Entity<Mass>("::flecs.units.Mass");
                world.Entity<Force>("::flecs.units.Force");
                world.Entity<ElectricCurrent>("::flecs.units.ElectricCurrent");
                world.Entity<Amount>("::flecs.units.Amount");
                world.Entity<LuminousIntensity>("::flecs.units.LuminousIntensity");
                world.Entity<Length>("::flecs.units.Length");
                world.Entity<Pressure>("::flecs.units.Pressure");
                world.Entity<Speed>("::flecs.units.Speed");
                world.Entity<Temperature>("::flecs.units.Temperature");
                world.Entity<Data>("::flecs.units.Data");
                world.Entity<DataRate>("::flecs.units.DataRate");
                world.Entity<Angle>("::flecs.units.Angle");
                world.Entity<Frequency>("::flecs.units.Frequency");
                world.Entity<Uri>("::flecs.units.Uri");

                world.Entity<Durations.PicoSeconds>("::flecs.units.Duration.PicoSeconds");
                world.Entity<Durations.NanoSeconds>("::flecs.units.Duration.NanoSeconds");
                world.Entity<Durations.MicroSeconds>("::flecs.units.Duration.MicroSeconds");
                world.Entity<Durations.MilliSeconds>("::flecs.units.Duration.MilliSeconds");
                world.Entity<Durations.Seconds>("::flecs.units.Duration.Seconds");
                world.Entity<Durations.Minutes>("::flecs.units.Duration.Minutes");
                world.Entity<Durations.Hours>("::flecs.units.Duration.Hours");
                world.Entity<Durations.Days>("::flecs.units.Duration.Days");

                world.Entity<Times.CalenderDate>("::flecs.units.Time.Date");

                world.Entity<Masses.Grams>("::flecs.units.Mass.Grams");
                world.Entity<Masses.KiloGrams>("::flecs.units.Mass.KiloGrams");

                world.Entity<ElectricCurrents.Ampere>("::flecs.units.ElectricCurrent.Ampere");

                world.Entity<Amounts.Mole>("::flecs.units.Amount.Mole");

                world.Entity<LuminousIntensities.Candela>("::flecs.units.LuminousIntensity.Candela");

                world.Entity<Forces.Newton>("::flecs.units.Force.Newton");

                world.Entity<Lengths.Meters>("::flecs.units.Length.Meters");
                world.Entity<Lengths.PicoMeters>("::flecs.units.Length.PicoMeters");
                world.Entity<Lengths.NanoMeters>("::flecs.units.Length.NanoMeters");
                world.Entity<Lengths.MicroMeters>("::flecs.units.Length.MicroMeters");
                world.Entity<Lengths.MilliMeters>("::flecs.units.Length.MilliMeters");
                world.Entity<Lengths.CentiMeters>("::flecs.units.Length.CentiMeters");
                world.Entity<Lengths.KiloMeters>("::flecs.units.Length.KiloMeters");
                world.Entity<Lengths.Miles>("::flecs.units.Length.Miles");
                world.Entity<Lengths.Pixels>("::flecs.units.Length.Pixels");

                world.Entity<Pressures.Pascal>("::flecs.units.Pressure.Pascal");
                world.Entity<Pressures.Bar>("::flecs.units.Pressure.Bar");

                world.Entity<Speeds.MetersPerSecond>("::flecs.units.Speed.MetersPerSecond");
                world.Entity<Speeds.KiloMetersPerSecond>("::flecs.units.Speed.KiloMetersPerSecond");
                world.Entity<Speeds.KiloMetersPerHour>("::flecs.units.Speed.KiloMetersPerHour");
                world.Entity<Speeds.MilesPerHour>("::flecs.units.Speed.MilesPerHour");

                world.Entity<Temperatures.Kelvin>("::flecs.units.Temperature.Kelvin");
                world.Entity<Temperatures.Celsius>("::flecs.units.Temperature.Celsius");
                world.Entity<Temperatures.Fahrenheit>("::flecs.units.Temperature.Fahrenheit");

                world.Entity<Datas.Bits>("::flecs.units.Data.Bits");
                world.Entity<Datas.KiloBits>("::flecs.units.Data.KiloBits");
                world.Entity<Datas.MegaBits>("::flecs.units.Data.MegaBits");
                world.Entity<Datas.GigaBits>("::flecs.units.Data.GigaBits");
                world.Entity<Datas.Bytes>("::flecs.units.Data.Bytes");
                world.Entity<Datas.KiloBytes>("::flecs.units.Data.KiloBytes");
                world.Entity<Datas.MegaBytes>("::flecs.units.Data.MegaBytes");
                world.Entity<Datas.GigaBytes>("::flecs.units.Data.GigaBytes");
                world.Entity<Datas.KibiBytes>("::flecs.units.Data.KibiBytes");
                world.Entity<Datas.MebiBytes>("::flecs.units.Data.MebiBytes");
                world.Entity<Datas.GibiBytes>("::flecs.units.Data.GibiBytes");

                world.Entity<DataRates.BitsPerSecond>("::flecs.units.DataRate.BitsPerSecond");
                world.Entity<DataRates.KiloBitsPerSecond>("::flecs.units.DataRate.KiloBitsPerSecond");
                world.Entity<DataRates.MegaBitsPerSecond>("::flecs.units.DataRate.MegaBitsPerSecond");
                world.Entity<DataRates.GigaBitsPerSecond>("::flecs.units.DataRate.GigaBitsPerSecond");
                world.Entity<DataRates.BytesPerSecond>("::flecs.units.DataRate.BytesPerSecond");
                world.Entity<DataRates.KiloBytesPerSecond>("::flecs.units.DataRate.KiloBytesPerSecond");
                world.Entity<DataRates.MegaBytesPerSecond>("::flecs.units.DataRate.MegaBytesPerSecond");
                world.Entity<DataRates.GigaBytesPerSecond>("::flecs.units.DataRate.GigaBytesPerSecond");

                world.Entity<Frequencies.Hertz>("::flecs.units.Frequency.Hertz");
                world.Entity<Frequencies.KiloHertz>("::flecs.units.Frequency.KiloHertz");
                world.Entity<Frequencies.MegaHertz>("::flecs.units.Frequency.MegaHertz");
                world.Entity<Frequencies.GigaHertz>("::flecs.units.Frequency.GigaHertz");

                world.Entity<Uris.Hyperlink>("::flecs.units.Uri.Hyperlink");
                world.Entity<Uris.Image>("::flecs.units.Uri.Image");
                world.Entity<Uris.File>("::flecs.units.Uri.File");

                world.Entity<Angles.Radians>("::flecs.units.Angle.Radians");
                world.Entity<Angles.Degrees>("::flecs.units.Angle.Degrees");

                world.Entity<Percentage>("::flecs.units.Percentage");

                world.Entity<Bel>("::flecs.units.Bel");
                world.Entity<DeciBel>("::flecs.units.DeciBel");
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

            /// <summary>
            ///     Checks if two <see cref="Units"/> instances are equal.
            /// </summary>
            /// <param name="other"></param>
            /// <returns></returns>
            public bool Equals(Units other)
            {
                return true;
            }

            /// <summary>
            ///     Checks if two <see cref="Units"/> instances are equal.
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object? obj)
            {
                return obj is Units;
            }

            /// <summary>
            ///     Returns the hash code of teh <see cref="Units"/>.
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                return 0;
            }

            /// <summary>
            ///     Checks if two <see cref="Units"/> instances are equal.
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            /// <returns></returns>
            public static bool operator ==(Units left, Units right)
            {
                return left.Equals(right);
            }

            /// <summary>
            ///     Checks if two <see cref="Units"/> instances are not equal.
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            /// <returns></returns>
            public static bool operator !=(Units left, Units right)
            {
                return !(left == right);
            }
        }
    }
}
