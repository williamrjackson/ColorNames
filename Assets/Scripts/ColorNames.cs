using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ColorNames
{
    private static List<NamedColor> namedColors = new List<NamedColor>();

    // Constructor
    public ColorNames()
    {
        ParseCSV(csv);
    }

    void ParseCSV(string csv)
    {
        string[] lines = csv.Split('\n');
        foreach (string line in lines)
        {
            if (!line.Contains("#"))
            {
                continue;
            }
            string[] vals = line.Split(',');
            NamedColor nc = new NamedColor();
            nc.name = vals[0].Trim();
            if (ColorUtility.TryParseHtmlString(vals[1], out Color newCol))
            {
                nc.color = newCol;
                namedColors.Add(nc);
            }
        }
    }
    public Color GetColor(string name)
    {
        NamedColor nc = namedColors.FirstOrDefault(x => x.name == name);
        if (nc != null)
        {
            return nc.color;
        }
        return Color.white;
    }
    public string GetName(Color color)
    {
        NamedColor nc = namedColors.FirstOrDefault(x => x.color == color);
        if (nc != null)
        {
            return nc.name;
        }
        return ClosestColor(color).name;
    }

    NamedColor ClosestColor(Color target)
    {
        var colorDiffs = namedColors.Select(n => ColorDifference(n.color, target)).Min(n =>n);
        return namedColors[namedColors.FindIndex(n => ColorDifference(n.color, target) == colorDiffs)];
    }
    private float ColorDifference(Color targetColor, Color testColor, float satOffset = 0, float valOffset = 0)
	{
        // https://en.wikipedia.org/wiki/Euclidean_distance
        if (satOffset + valOffset != 0)
        {
            targetColor = StripSV(targetColor, satOffset, valOffset);
            testColor = StripSV(testColor, satOffset, valOffset);
        }

        float redDiff = testColor.r - targetColor.r;
		float greenDiff = testColor.g - targetColor.g;
		float blueDiff = testColor.b - targetColor.b;

        float totalDiff = redDiff * redDiff + greenDiff * greenDiff + blueDiff * blueDiff;
        return totalDiff / 3;
	}
    private Color StripSV(Color inColor, float sOffset, float vOffset)
    {
        float colorH;
        float colorS;
        float colorV;

        Color.RGBToHSV(inColor, out colorH, out colorS, out colorV);

        return Color.HSVToRGB(colorH, Mathf.Clamp01(colorS + sOffset), Mathf.Clamp01(colorV + vOffset));
    }
    private class NamedColor
    {
        public string name;
        public Color color;
    }

    // From https://en.wikipedia.org/wiki/List_of_colors_(alphabetical)#List_of_colors
    private static string csv = @"Absolute Zero,#0048BA
        Acid green,#B0BF1A
        Aero,#7CB9E8
        African violet,#B284BE
        Air superiority blue,#72A0C1
        Alice blue,#F0F8FF
        Alizarin,#DB2D43
        Alloy orange,#C46210
        Almond,#EED9C4
        Amaranth deep purple,#9F2B68
        Amaranth pink,#F19CBB
        Amaranth purple,#AB274F
        Amazon,#3B7A57
        Amber,#FFBF00
        Amethyst,#9966CC
        Android green,#3DDC84
        Antique brass,#C88A65
        Antique bronze,#665D1E
        Antique fuchsia,#915C83
        Antique ruby,#841B2D
        Antique white,#FAEBD7
        Apricot,#FBCEB1
        Aqua,#00FFFF
        Aquamarine,#7FFFD4
        Arctic lime,#D0FF14
        Artichoke green,#4B6F44
        Arylide yellow,#E9D66B
        Ash gray,#B2BEB5
        Atomic tangerine,#FF9966
        Aureolin,#FDEE00
        Azure,#007FFF
        Azure (X11/web color),#F0FFFF
        Baby blue,#89CFF0
        Baby blue eyes,#A1CAF1
        Baby pink,#F4C2C2
        Baby powder,#FEFEFA
        Baker-Miller pink,#FF91AF
        Banana Mania,#FAE7B5
        Barbie Pink,#DA1884
        Barn red,#7C0A02
        Battleship grey,#848482
        Beau blue,#BCD4E6
        Beaver,#9F8170
        Beige,#F5F5DC
        B'dazzled blue,#2E5894
        Big dip o’ruby,#9C2542
        Bisque,#FFE4C4
        Bistre,#3D2B1F
        Bistre brown,#967117
        Bitter lemon,#CAE00D
        Black,#000000
        Black bean,#3D0C02
        Black coral,#54626F
        Black olive,#3B3C36
        Black Shadows,#BFAFB2
        Blanched almond,#FFEBCD
        Blast-off bronze,#A57164
        Bleu de France,#318CE7
        Blizzard blue,#ACE5EE
        Blood red,#660000
        Blue,#0000FF
        Blue (Crayola),#1F75FE
        Blue (Munsell),#0093AF
        Blue (NCS),#0087BD
        Blue (Pantone),#0018A8
        Blue (pigment),#333399
        Blue bell,#A2A2D0
        Blue-gray (Crayola),#6699CC
        Blue jeans,#5DADEC
        Blue sapphire,#126180
        Blue-violet,#8A2BE2
        Blue yonder,#5072A7
        Bluetiful,#3C69E7
        Blush,#DE5D83
        Bole,#79443B
        Bone,#E3DAC9
        Brick red,#CB4154
        Bright lilac,#D891EF
        Bright yellow (Crayola),#FFAA1D
        British racing green,#004225
        Bronze,#CD7F32
        Brown,#964B00
        Brown sugar,#AF6E4D
        Bud green,#7BB661
        Buff,#FFC680
        Burgundy,#800020
        Burlywood,#DEB887
        Burnished brown,#A17A74
        Burnt orange,#CC5500
        Burnt sienna,#E97451
        Burnt umber,#8A3324
        Byzantine,#BD33A4
        Byzantium,#702963
        Cadet blue,#5F9EA0
        Cadet grey,#91A3B0
        Cadmium green,#006B3C
        Cadmium orange,#ED872D
        Café au lait,#A67B5B
        Café noir,#4B3621
        Cambridge blue,#A3C1AD
        Camel,#C19A6B
        Cameo pink,#EFBBCC
        Canary,#FFFF99
        Canary yellow,#FFEF00
        Candy pink,#E4717A
        Cardinal,#C41E3A
        Caribbean green,#00CC99
        Carmine,#960018
        Carmine (M&P),#D70040
        Carnation pink,#FFA6C9
        Carnelian,#B31B1B
        Carolina blue,#56A0D3
        Carrot orange,#ED9121
        Catawba,#703642
        Cedar Chest,#C95A49
        Celadon,#ACE1AF
        Celeste,#B2FFFF
        Cerise,#DE3163
        Cerulean,#007BA7
        Cerulean blue,#2A52BE
        Cerulean frost,#6D9BC3
        Cerulean (Crayola),#1DACD6
        Cerulean (RGB),#0040FF
        Champagne,#F7E7CE
        Champagne pink,#F1DDCF
        Charcoal,#36454F
        Charm pink,#E68FAC
        Chartreuse (web),#80FF00
        Cherry blossom pink,#FFB7C5
        Chestnut,#954535
        Chili red,#E23D28
        China pink,#DE6FA1
        Chinese red,#AA381E
        Chinese violet,#856088
        Chinese yellow,#FFB200
        Chocolate (traditional),#7B3F00
        Chocolate (web),#D2691E
        Cinereous,#98817B
        Cinnabar,#E34234
        Cinnamon Satin,#CD607E
        Citrine,#E4D00A
        Citron,#9FA91F
        Claret,#7F1734
        Coffee,#6F4E37
        Columbia Blue,#B9D9EB
        Congo pink,#F88379
        Cool grey,#8C92AC
        Copper,#B87333
        Copper (Crayola),#DA8A67
        Copper penny,#AD6F69
        Copper red,#CB6D51
        Copper rose,#996666
        Coquelicot,#FF3800
        Coral,#FF7F50
        Coral pink,#F88379
        Cordovan,#893F45
        Corn,#FBEC5D
        Cornflower blue,#6495ED
        Cornsilk,#FFF8DC
        Cosmic cobalt,#2E2D88
        Cosmic latte,#FFF8E7
        Coyote brown,#81613C
        Cotton candy,#FFBCD9
        Cream,#FFFDD0
        Crimson,#DC143C
        Crimson (UA),#9E1B32
        Cultured Pearl,#F5F5F5
        Cyan,#00FFFF
        Cyan (process),#00B7EB
        Cyber grape,#58427C
        Cyber yellow,#FFD300
        Cyclamen,#F56FA1
        Dandelion,#FED85D
        Dark brown,#654321
        Dark byzantium,#5D3954
        Dark cyan,#008B8B
        Dark electric blue,#536878
        Dark goldenrod,#B8860B
        Dark green (X11),#006400
        Dark jungle green,#1A2421
        Dark khaki,#BDB76B
        Dark lava,#483C32
        Dark liver (horses),#543D37
        Dark magenta,#8B008B
        Dark olive green,#556B2F
        Dark orange,#FF8C00
        Dark orchid,#9932CC
        Dark purple,#301934
        Dark red,#8B0000
        Dark salmon,#E9967A
        Dark sea green,#8FBC8F
        Dark sienna,#3C1414
        Dark sky blue,#8CBED6
        Dark slate blue,#483D8B
        Dark slate gray,#2F4F4F
        Dark spring green,#177245
        Dark turquoise,#00CED1
        Dark violet,#9400D3
        Davy's grey,#555555
        Deep cerise,#DA3287
        Deep champagne,#FAD6A5
        Deep chestnut,#B94E48
        Deep jungle green,#004B49
        Deep pink,#FF1493
        Deep saffron,#FF9933
        Deep sky blue,#00BFFF
        Deep Space Sparkle,#4A646C
        Deep taupe,#7E5E60
        Denim,#1560BD
        Denim blue,#2243B6
        Desert,#C19A6B
        Desert sand,#EDC9AF
        Dim gray,#696969
        Dodger blue,#1E90FF
        Drab dark brown,#4A412A
        Duke blue,#00009C
        Dutch white,#EFDFBB
        Ebony,#555D50
        Ecru,#C2B280
        Eerie black,#1B1B1B
        Eggplant,#614051
        Eggshell,#F0EAD6
        Electric lime,#CCFF00
        Electric purple,#BF00FF
        Electric violet,#8F00FF
        Emerald,#50C878
        Eminence,#6C3082
        English lavender,#B48395
        English red,#AB4B52
        English vermillion,#CC474B
        English violet,#563C5C
        Erin,#00FF40
        Eton blue,#96C8A2
        Fallow,#C19A6B
        Falu red,#801818
        Fandango,#B53389
        Fandango pink,#DE5285
        Fawn,#E5AA70
        Fern green,#4F7942
        Field drab,#6C541E
        Fiery rose,#FF5470
        Finn,#683068
        Firebrick,#B22222
        Fire engine red,#CE2029
        Flame,#E25822
        Flax,#EEDC82
        Flirt,#A2006D
        Floral white,#FFFAF0
        Forest green (web),#228B22
        French beige,#A67B5B
        French bistre,#856D4D
        French blue,#0072BB
        French fuchsia,#FD3F92
        French lilac,#86608E
        French lime,#9EFD38
        French mauve,#D473D4
        French pink,#FD6C9E
        French raspberry,#C72C48
        French sky blue,#77B5FE
        French violet,#8806CE
        Frostbite,#E936A7
        Fuchsia,#FF00FF
        Fuchsia (Crayola),#C154C1
        Fulvous,#E48400
        Fuzzy Wuzzy,#87421F
        Gainsboro,#DCDCDC
        Gamboge,#E49B0F
        Generic viridian,#007F66
        Ghost white,#F8F8FF
        Glaucous,#6082B6
        Glossy grape,#AB92B3
        GO green,#00AB66
        Gold (metallic),#D4AF37
        Gold (web) (Golden),#FFD700
        Gold (Crayola),#E6BE8A
        Gold Fusion,#85754E
        Golden brown,#996515
        Golden poppy,#FCC200
        Golden yellow,#FFDF00
        Goldenrod,#DAA520
        Gotham green,#00573F
        Granite gray,#676767
        Granny Smith apple,#A8E4A0
        Gray (web),#808080
        Gray (X11 gray),#BEBEBE
        Green,#00FF00
        Green (Crayola),#1CAC78
        Green (web),#008000
        Green (Munsell),#00A877
        Green (NCS),#009F6B
        Green (Pantone),#00AD43
        Green (pigment),#00A550
        Green-blue,#1164B4
        Green Lizard,#A7F432
        Green Sheen,#6EAEA1
        Gunmetal,#2a3439
        Hansa yellow,#E9D66B
        Harlequin,#3FFF00
        Harvest gold,#DA9100
        Heat Wave,#FF7A00
        Heliotrope,#DF73FF
        Heliotrope gray,#AA98A9
        Hollywood cerise,#F400A1
        Honolulu blue,#006DB0
        Hooker's green,#49796B
        Hot magenta,#FF1DCE
        Hot pink,#FF69B4
        Hunter green,#355E3B
        Iceberg,#71A6D2
        Illuminating emerald,#319177
        Imperial red,#ED2939
        Inchworm,#B2EC5D
        Independence,#4C516D
        India green,#138808
        Indian red,#CD5C5C
        Indian yellow,#E3A857
        Indigo,#4B0082
        Indigo dye,#00416A
        International Klein Blue,#130a8f
        International orange (engineering),#BA160C
        International orange (Golden Gate Bridge),#C0362C
        Irresistible,#B3446C
        Isabelline,#F4F0EC
        Italian sky blue,#B2FFFF
        Ivory,#FFFFF0
        Japanese carmine,#9D2933
        Japanese violet,#5B3256
        Jasmine,#F8DE7E
        Jazzberry jam,#A50B5E
        Jet,#343434
        Jonquil,#F4CA16
        June bud,#BDDA57
        Jungle green,#29AB87
        Kelly green,#4CBB17
        Keppel,#3AB09E
        Key lime,#E8F48C
        Khaki (web),#C3B091
        Khaki (X11) (Light khaki),#F0E68C
        Kobe,#882D17
        Kobi,#E79FC4
        Kobicha,#6B4423
        KSU purple,#512888
        Languid lavender,#D6CADD
        Lapis lazuli,#26619C
        Laser lemon,#FFFF66
        Laurel green,#A9BA9D
        Lava,#CF1020
        Lavender (floral),#B57EDC
        Lavender (web),#E6E6FA
        Lavender blue,#CCCCFF
        Lavender blush,#FFF0F5
        Lavender gray,#C4C3D0
        Lawn green,#7CFC00
        Lemon,#FFF700
        Lemon chiffon,#FFFACD
        Lemon curry,#CCA01D
        Lemon glacier,#FDFF00
        Lemon meringue,#F6EABE
        Lemon yellow,#FFF44F
        Lemon yellow (Crayola),#FFFF9F
        Liberty,#545AA7
        Light blue,#ADD8E6
        Light coral,#F08080
        Light cornflower blue,#93CCEA
        Light cyan,#E0FFFF
        Light French beige,#C8AD7F
        Light goldenrod yellow,#FAFAD2
        Light gray,#D3D3D3
        Light green,#90EE90
        Light orange,#FED8B1
        Light periwinkle,#C5CBE1
        Light pink,#FFB6C1
        Light salmon,#FFA07A
        Light sea green,#20B2AA
        Light sky blue,#87CEFA
        Light slate gray,#778899
        Light steel blue,#B0C4DE
        Light yellow,#FFFFE0
        Lilac,#C8A2C8
        Lilac Luster,#AE98AA
        Lime (color wheel),#BFFF00
        Lime (web) (X11 green),#00FF00
        Lime green,#32CD32
        Lincoln green,#195905
        Linen,#FAF0E6
        Lion,#DECC9C
        Liseran purple,#DE6FA1
        Little boy blue,#6CA0DC
        Liver,#674C47
        Liver (dogs),#B86D29
        Liver (organ),#6C2E1F
        Liver chestnut,#987456
        Livid,#6699CC
        Macaroni and Cheese,#FFBD88
        Madder Lake,#CC3336
        Magenta,#FF00FF
        Magenta (Crayola),#F653A6
        Magenta (dye),#CA1F7B
        Magenta (Pantone),#D0417E
        Magenta (process),#FF0090
        Magenta haze,#9F4576
        Magic mint,#AAF0D1
        Magnolia,#F2E8D7
        Mahogany,#C04000
        Maize,#FBEC5D
        Maize (Crayola),#F2C649
        Majorelle blue,#6050DC
        Malachite,#0BDA51
        Manatee,#979AAA
        Mandarin,#F37A48
        Mango,#FDBE02
        Mango Tango,#FF8243
        Mantis,#74C365
        Mardi Gras,#880085
        Marigold,#EAA221
        Maroon (Crayola),#C32148
        Maroon (web),#800000
        Maroon (X11),#B03060
        Mauve,#E0B0FF
        Mauve taupe,#915F6D
        Mauvelous,#EF98AA
        Maximum blue,#47ABCC
        Maximum blue green,#30BFBF
        Maximum blue purple,#ACACE6
        Maximum green,#5E8C31
        Maximum green yellow,#D9E650
        Maximum purple,#733380
        Maximum red,#D92121
        Maximum red purple,#A63A79
        Maximum yellow,#FAFA37
        Maximum yellow red,#F2BA49
        May green,#4C9141
        Maya blue,#73C2FB
        Medium aquamarine,#66DDAA
        Medium blue,#0000CD
        Medium candy apple red,#E2062C
        Medium carmine,#AF4035
        Medium champagne,#F3E5AB
        Medium orchid,#BA55D3
        Medium purple,#9370DB
        Medium sea green,#3CB371
        Medium slate blue,#7B68EE
        Medium spring green,#00FA9A
        Medium turquoise,#48D1CC
        Medium violet-red,#C71585
        Mellow apricot,#F8B878
        Mellow yellow,#F8DE7E
        Melon,#FEBAAD
        Metallic gold,#D3AF37
        Metallic Seaweed,#0A7E8C
        Metallic Sunburst,#9C7C38
        Mexican pink,#E4007C
        Middle blue,#7ED4E6
        Middle blue green,#8DD9CC
        Middle blue purple,#8B72BE
        Middle grey,#8B8680
        Middle green,#4D8C57
        Middle green yellow,#ACBF60
        Middle purple,#D982B5
        Middle red,#E58E73
        Middle red purple,#A55353
        Middle yellow,#FFEB00
        Middle yellow red,#ECB176
        Midnight,#702670
        Midnight blue,#191970
        Midnight green (eagle green),#004953
        Mikado yellow,#FFC40C
        Mimi pink,#FFDAE9
        Mindaro,#E3F988
        Ming,#36747D
        Minion yellow,#F5E050
        Mint,#3EB489
        Mint cream,#F5FFFA
        Mint green,#98FF98
        Misty moss,#BBB477
        Misty rose,#FFE4E1
        Mode beige,#967117
        Mona Lisa,#FF948E
        Morning blue,#8DA399
        Moss green,#8A9A5B
        Mountain Meadow,#30BA8F
        Mountbatten pink,#997A8D
        MSU green,#18453B
        Mulberry,#C54B8C
        Mulberry (Crayola),#C8509B
        Mustard,#FFDB58
        Myrtle green,#317873
        Mystic,#D65282
        Mystic maroon,#AD4379
        Nadeshiko pink,#F6ADC6
        Naples yellow,#FADA5E
        Navajo white,#FFDEAD
        Navy blue,#000080
        Navy blue (Crayola),#1974D2
        Neon blue,#4666FF
        Neon green,#39FF14
        Neon fuchsia,#FE4164
        New Car,#214FC6
        New York pink,#D7837F
        Nickel,#727472
        Non-photo blue,#A4DDED
        Nyanza,#E9FFDB
        Ochre,#CC7722
        Old burgundy,#43302E
        Old gold,#CFB53B
        Old lace,#FDF5E6
        Old lavender,#796878
        Old mauve,#673147
        Old rose,#C08081
        Old silver,#848482
        Olive,#808000
        Olive Drab (,
        Olive Drab ,#7
        Olive green,#B5B35C
        Olivine,#9AB973
        Onyx,#353839
        Opal,#A8C3BC
        Opera mauve,#B784A7
        Orange,#FF7F00
        Orange (Crayola),#FF7538
        Orange (Pantone),#FF5800
        Orange (web),#FFA500
        Orange peel,#FF9F00
        Orange-red,#FF681F
        Orange-red (Crayola),#FF5349
        Orange soda,#FA5B3D
        Orange-yellow,#F5BD1F
        Orange-yellow (Crayola),#F8D568
        Orchid,#DA70D6
        Orchid pink,#F2BDCD
        Orchid (Crayola),#E29CD2
        Outer space (Crayola),#2D383A
        Outrageous Orange,#FF6E4A
        Oxblood,#4A0000
        Oxford blue,#002147
        OU Crimson red,#841617
        Pacific blue,#1CA9C9
        Pakistan green,#006600
        Palatinate purple,#682860
        Pale aqua,#BED3E5
        Pale cerulean,#9BC4E2
        Pale Dogwood,#ED7A9B
        Pale pink,#FADADD
        Pale purple (Pantone),#FAE6FA
        Pale spring bud,#ECEBBD
        Pansy purple,#78184A
        Paolo Veronese green,#009B7D
        Papaya whip,#FFEFD5
        Paradise pink,#E63E62
        Parchment,#F1E9D2
        Paris Green,#50C878
        Pastel pink,#DEA5A4
        Patriarch,#800080
        Paua,#1F005E
        Payne's grey,#536878
        Peach,#FFE5B4
        Peach (Crayola),#FFCBA4
        Peach puff,#FFDAB9
        Pear,#D1E231
        Pearly purple,#B768A2
        Periwinkle,#CCCCFF
        Periwinkle (Crayola),#C3CDE6
        Permanent Geranium Lake,#E12C2C
        Persian blue,#1C39BB
        Persian green,#00A693
        Persian indigo,#32127A
        Persian orange,#D99058
        Persian pink,#F77FBE
        Persian plum,#701C1C
        Persian red,#CC3333
        Persian rose,#FE28A2
        Persimmon,#EC5800
        Pewter Blue,#8BA8B7
        Phlox,#DF00FF
        Phthalo blue,#000F89
        Phthalo green,#123524
        Picotee blue,#2E2787
        Pictorial carmine,#C30B4E
        Piggy pink,#FDDDE6
        Pine green,#01796F
        Pine green,#2A2F23
        Pink,#FFC0CB
        Pink (Pantone),#D74894
        Pink lace,#FFDDF4
        Pink lavender,#D8B2D1
        Pink Sherbet,#F78FA7
        Pistachio,#93C572
        Platinum,#E5E4E2
        Plum,#8E4585
        Plum (web),#DDA0DD
        Plump Purple,#5946B2
        Polished Pine,#5DA493
        Pomp and Power,#86608E
        Popstar,#BE4F62
        Portland Orange,#FF5A36
        Powder blue,#B0E0E6
        Prairie gold,#E1CA7A
        Princeton orange,#F58025
        Prune,#701C1C
        Prussian blue,#003153
        Psychedelic purple,#DF00FF
        Puce,#CC8899
        Pullman Brown (UPS Brown),#644117
        Pumpkin,#FF7518
        Purple,#6A0DAD
        Purple (web),#800080
        Purple (Munsell),#9F00C5
        Purple (X11),#A020F0
        Purple mountain majesty,#9678B6
        Purple navy,#4E5180
        Purple pizzazz,#FE4EDA
        Purple Plum,#9C51B6
        Purpureus,#9A4EAE
        Queen blue,#436B95
        Queen pink,#E8CCD7
        Quick Silver,#A6A6A6
        Quinacridone magenta,#8E3A59
        Radical Red,#FF355E
        Raisin black,#242124
        Rajah,#FBAB60
        Raspberry,#E30B5D
        Raspberry glacé,#915F6D
        Raspberry rose,#B3446C
        Raw sienna,#D68A59
        Raw umber,#826644
        Razzle dazzle rose,#FF33CC
        Razzmatazz,#E3256B
        Razzmic Berry,#8D4E85
        Rebecca Purple,#663399
        Red,#FF0000
        Red (Crayola),#EE204D
        Red (Munsell),#F2003C
        Red (NCS),#C40233
        Red (Pantone),#ED2939
        Red (pigment),#ED1C24
        Red (RYB),#FE2712
        Red-orange,#FF5349
        Red-orange (Crayola),#FF681F
        Red-orange (Color wheel),#FF4500
        Red-purple,#E40078
        Red Salsa,#FD3A4A
        Red-violet,#C71585
        Red-violet (Crayola),#C0448F
        Red-violet (Color wheel),#922B3E
        Redwood,#A45A52
        Resolution blue,#002387
        Rhythm,#777696
        Rich black,#004040
        Rich black (FOGRA29),#010B13
        Rich black (FOGRA39),#010203
        Rifle green,#444C38
        Robin egg blue,#00CCCC
        Rocket metallic,#8A7F80
        Rojo Spanish red,#A91101
        Roman silver,#838996
        Rose,#FF007F
        Rose bonbon,#F9429E
        Rose Dust,#9E5E6F
        Rose ebony,#674846
        Rose madder,#E32636
        Rose pink,#FF66CC
        Rose Pompadour,#ED7A9B
        Rose red,#C21E56
        Rose taupe,#905D5D
        Rose vale,#AB4E52
        Rosewood,#65000B
        Rosso corsa,#D40000
        Rosy brown,#BC8F8F
        Royal blue (dark),#002366
        Royal blue (light),#4169E1
        Royal purple,#7851A9
        Royal yellow,#FADA5E
        Ruber,#CE4676
        Rubine red,#D10056
        Ruby,#E0115F
        Ruby red,#9B111E
        Rufous,#A81C07
        Russet,#80461B
        Russian green,#679267
        Russian violet,#32174D
        Rust,#B7410E
        Rusty red,#DA2C43
        Sacramento State green,#043927
        Saddle brown,#8B4513
        Safety orange,#FF7800
        Safety orange (blaze orange),#FF6700
        Safety yellow,#EED202
        Saffron,#F4C430
        Sage,#BCB88A
        St. Patrick's blue,#23297A
        Salmon,#FA8072
        Salmon pink,#FF91A4
        Sand,#C2B280
        Sand dune,#967117
        Sandy brown,#F4A460
        Sap green,#507D2A
        Sapphire,#0F52BA
        Sapphire blue,#0067A5
        Sapphire (Crayola),#2D5DA1
        Satin sheen gold,#CBA135
        Scarlet,#FF2400
        Schauss pink,#FF91AF
        School bus yellow,#FFD800
        Screamin' Green,#66FF66
        Sea green,#2E8B57
        Sea green (Crayola),#00FFCD
        Seance,#612086
        Seal brown,#59260B
        Seashell,#FFF5EE
        Secret,#764374
        Selective yellow,#FFBA00
        Sepia,#704214
        Shadow,#8A795D
        Shadow blue,#778BA5
        Shamrock green,#009E60
        Sheen green,#8FD400
        Shimmering Blush,#D98695
        Shiny Shamrock,#5FA778
        Shocking pink,#FC0FC0
        Shocking pink (Crayola),#FF6FFF
        Sienna,#882D17
        Silver,#C0C0C0
        Silver (Crayola),#C9C0BB
        Silver (Metallic),#AAA9AD
        Silver chalice,#ACACAC
        Silver pink,#C4AEAD
        Silver sand,#BFC1C2
        Sinopia,#CB410B
        Sizzling Red,#FF3855
        Sizzling Sunrise,#FFDB00
        Skobeloff,#007474
        Sky blue,#87CEEB
        Sky blue (Crayola),#76D7EA
        Sky magenta,#CF71AF
        Slate blue,#6A5ACD
        Slate gray,#708090
        Slimy green,#299617
        Smitten,#C84186
        Smoky black,#100C08
        Snow,#FFFAFA
        Solid pink,#893843
        Sonic silver,#757575
        Space cadet,#1D2951
        Spanish bistre,#807532
        Spanish blue,#0070B8
        Spanish carmine,#D10047
        Spanish gray,#989898
        Spanish green,#009150
        Spanish orange,#E86100
        Spanish pink,#F7BFBE
        Spanish red,#E60026
        Spanish sky blue,#00FFFE
        Spanish violet,#4C2882
        Spanish viridian,#007F5C
        Spring bud,#A7FC00
        Spring Frost,#87FF2A
        Spring green,#00FF7F
        Spring green (Crayola),#ECEBBD
        Star command blue,#007BB8
        Steel blue,#4682B4
        Steel pink,#CC33CC
        Stil de grain yellow,#FADA5E
        Straw,#E4D96F
        Strawberry,#FA5053
        Strawberry Blonde,#FF9361
        Strong Lime Green,#33CC33
        Sugar Plum,#914E75
        Sunglow,#FFCC33
        Sunray,#E3AB57
        Sunset,#FAD6A5
        Super pink,#CF6BA9
        Sweet Brown,#A83731
        Syracuse Orange,#D44500
        Tan,#D2B48C
        Tan (Crayola),#D99A6C
        Tangerine,#F28500
        Tango pink,#E4717A
        Tart Orange,#FB4D46
        Taupe,#483C32
        Taupe gray,#8B8589
        Tea green,#D0F0C0
        Tea rose,#F4C2C2
        Teal,#008080
        Teal blue,#367588
        Technobotanica,#00FFBF
        Telemagenta,#CF3476
        Tenné (tawny),#CD5700
        Terra cotta,#E2725B
        Thistle,#D8BFD8
        Thulian pink,#DE6FA1
        Tickle Me Pink,#FC89AC
        Tiffany Blue,#0ABAB5
        Timberwolf,#DBD7D2
        Titanium yellow,#EEE600
        Tomato,#FF6347
        Tourmaline,#86A1A9
        Tropical rainforest,#00755E
        True Blue,#2D68C4
        Trypan Blue,#1C05B3
        Tufts blue,#3E8EDE
        Tumbleweed,#DEAA88
        Turquoise,#40E0D0
        Turquoise blue,#00FFEF
        Turquoise green,#A0D6B4
        Turtle green,#8A9A5B
        Tuscan,#FAD6A5
        Tuscan brown,#6F4E37
        Tuscan red,#7C4848
        Tuscan tan,#A67B5B
        Tuscany,#C09999
        Twilight lavender,#8A496B
        Tyrian purple,#66023C
        UA blue,#0033AA
        UA red,#D9004C
        Ultramarine,#3F00FF
        Ultramarine blue,#4166F5
        Ultra pink,#FF6FFF
        Ultra red,#FC6C85
        Umber,#635147
        Unbleached silk,#FFDDCA
        United Nations blue,#009EDB
        University of Pennsylvania red,#A50021
        Unmellow yellow,#FFFF66
        UP Forest green,#014421
        UP maroon,#7B1113
        Upsdell red,#AE2029
        Uranian blue,#AFDBF5
        USAFA blue,#004F98
        Van Dyke brown,#664228
        Vanilla,#F3E5AB
        Vanilla ice,#F38FA9
        Vegas gold,#C5B358
        Venetian red,#C80815
        Verdigris,#43B3AE
        Vermilion,#E34234
        Vermilion,#D9381E
        Veronica,#A020F0
        Violet,#8F00FF
        Violet (color wheel),#7F00FF
        Violet (crayola),#963D7F
        Violet (RYB),#8601AF
        Violet (web),#EE82EE
        Violet-blue,#324AB2
        Violet-blue (Crayola),#766EC8
        Violet-red,#F75394
        Violet-red(PerBang),#F0599C
        Viridian,#40826D
        Viridian green,#009698
        Vivid burgundy,#9F1D35
        Vivid sky blue,#00CCFF
        Vivid tangerine,#FFA089
        Vivid violet,#9F00FF
        Volt,#CEFF00
        Warm black,#004242
        Weezy Blue,#189BCC
        Wheat,#F5DEB3
        White,#FFFFFF
        Wild blue yonder,#A2ADD0
        Wild orchid,#D470A2
        Wild Strawberry,#FF43A4
        Wild watermelon,#FC6C85
        Windsor tan,#A75502
        Wine,#722F37
        Wine dregs,#673147
        Winter Sky,#FF007C
        Wintergreen Dream,#56887D
        Wisteria,#C9A0DC
        Wood brown,#C19A6B
        Xanadu,#738678
        Xanthic,#EEED09
        Xanthous,#F1B42F
        Yale Blue,#00356B
        Yellow,#FFFF00
        Yellow (Crayola),#FCE883
        Yellow (Munsell),#EFCC00
        Yellow (NCS),#FFD300
        Yellow (Pantone),#FEDF00
        Yellow (process),#FFEF00
        Yellow (RYB),#FEFE33
        Yellow-green,#9ACD32
        Yellow-green (Crayola),#C5E384
        Yellow-green (Color Wheel),#30B21A
        Yellow Orange,#FFAE42
        Yellow Orange (Color Wheel),#FF9505
        Yellow Sunshine,#FFF700
        YInMn Blue,#2E5090
        Zaffre,#0014A8
        Zinnwaldite brown,#2C1608
        Zomp,#39A78E";
}
