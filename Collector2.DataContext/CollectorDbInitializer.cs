using Collector2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.DataContext
{
    public class CollectorDbInitializer : DropCreateDatabaseIfModelChanges<CollectorContext>
    {
        protected override void Seed(CollectorContext db)
        {
            base.Seed(db);
            var listOfCategories = new List<Category>()
            {
                new Category { CategoryName = "Action Adventure" },
                new Category { CategoryName = "Role Playing Game" },
                new Category { CategoryName = "Strategy" },
                new Category { CategoryName = "Point'n'Click" },
                new Category { CategoryName = "Sports - Football" },
                new Category { CategoryName = "Sports - Olympics" },
                new Category { CategoryName = "Sports - Futuristic" },
                new Category { CategoryName = "Sports - Tennis" },
                new Category { CategoryName = "Puzzle" },
                new Category { CategoryName = "Simulator - Flight" },
                new Category { CategoryName = "Platformer" },
                new Category { CategoryName = "Miscellanous" }
            };

            var listOfFormats = new List<Format>()
            {
                new Format { FormatName = "3.5\" Floppy Disk" },
                new Format { FormatName = "5.25\" Floppy Disk" },
                new Format { FormatName = "DVD" },
                new Format { FormatName = "CD" },
                new Format { FormatName = "C64 Cartridge" },
                new Format { FormatName = "SNES Cartridge" },
                new Format { FormatName = "Mega Drive Cartridge" },
                new Format { FormatName = "Gameboy Advance Cartridge"}
            };

            var listOfSpecs = new List<HardwareSpec>()
            {
                new HardwareSpec { HardwareSpecName = "Commodore Amiga 500", BasicSpecs = "512kb Chip Ram, 4096 Colors, 7 MHz Motorola 68000 Processor, OCS Chip Set"},
                new HardwareSpec { HardwareSpecName = "Commodore Amiga 1200", BasicSpecs = "2 MB Chip Ram, 256K Colors, 14.1 MHz Motorola 680EC20 Processor, AGA Chip Set"},
                new HardwareSpec { HardwareSpecName = "Windows PC", BasicSpecs = "N/A"},
                new HardwareSpec { HardwareSpecName = "Nintendo SNES", BasicSpecs = "N/A"},
                new HardwareSpec { HardwareSpecName = "Commodore 64", BasicSpecs = "64K Ram"}
            };

            db.HardwareSpec.AddRange(listOfSpecs);
            db.Category.AddRange(listOfCategories);
            db.Format.AddRange(listOfFormats);

            db.SaveChanges();
        }
    }
}
