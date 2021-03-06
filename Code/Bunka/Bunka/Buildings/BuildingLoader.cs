﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

public class BuildingLoader
{
    Dictionary<BuildingType, List<string[]>> buildings;

    public BuildingLoader()
    {
        // get all building stats files
        List<string> files = LoadDirectory("Content/Buildings");

        // initialise building dictionary
        this.buildings = new Dictionary<BuildingType, List<string[]>>();

        // load stats per file
        for (int i = 0; i < files.Count; i++)
        {
            List<string[]> current = new List<string[]>();
            BuildingType type;
            StreamReader reader = new StreamReader(files[i]);

            // read building type
            string line = reader.ReadLine();
            string[] stats = line.Split(new string[] { " = " }, System.StringSplitOptions.RemoveEmptyEntries);
            type = (BuildingType)(Enum.Parse(typeof(BuildingType), stats[1]));
            line = reader.ReadLine();

            // read other stats
            while (line != null)
            {
                stats = line.Split(new string[] { " = " }, System.StringSplitOptions.RemoveEmptyEntries);
                current.Add(stats);
                line = reader.ReadLine();
            }

            // add building to dictionary
            this.buildings.Add(type, current);

            reader.Close();
        }
    }

    // create a constructionhandler for a specific building
    public ConstructionHandler CreateConstructionRequest(BuildingType type, CPoint position)
    {
        List<string[]> building = this.buildings[type];

        // offset for buildings that have more lines preceding the construction information
        int offset = type > BuildingType.CONVERSION ? 2 : 0;

        int constructionTime = int.Parse(building[3 + offset][1]);
        string[] iTypes = building[4 + offset][1].Split(',');
        string[] iAmounts = building[5 + offset][1].Split(',');

        // parse construction cost types
        ResourceType[] costTypes = new ResourceType[iTypes.Length];
        for (int i = 0; i < iTypes.Length; i++)
            costTypes[i] = (ResourceType)(Enum.Parse(typeof(ResourceType), iTypes[i]));

        // parse construction cost amounts
        uint[] costAmounts = new uint[iAmounts.Length];
        for (int i = 0; i < iAmounts.Length; i++)
            costAmounts[i] = uint.Parse(iAmounts[i]);

        // create construction request
        return new ConstructionHandler(type, costTypes, costAmounts, constructionTime, position);
    }

    // create a building object of a specific type
    public Building CreateBuilding(BuildingType type, CPoint position)
    {
        if (type > BuildingType.CONVERSION)
            return CreateConversionBuilding(type, position);
        else if (type > BuildingType.PRODUCTION)
            return CreateProductionBuilding(type, position);
        else
            return null;
    }

    // method for creating conversion buildings
    private BuildingConversion CreateConversionBuilding(BuildingType type, CPoint position)
    {
        // stats for current building
        List<string[]> building = this.buildings[type];

        string[] oTypes = building[0][1].Split(',');
        string[] oAmounts = building[1][1].Split(',');
        string[] iTypes = building[2][1].Split(',');
        string[] iAmounts = building[3][1].Split(',');
        int speed = int.Parse(building[4][1]);

        // parse output types
        ResourceType[] outTypes = new ResourceType[oTypes.Length];
        for (int i = 0; i < oTypes.Length; i++)
            outTypes[i] = (ResourceType)(Enum.Parse(typeof(ResourceType), oTypes[i]));
        
        // parse output amounts
        byte[] outAmounts = new byte[oAmounts.Length];
        for (int i = 0; i < oAmounts.Length; i++)
            outAmounts[i] = byte.Parse(oAmounts[i]);

        // parse input types
        ResourceType[] inTypes = new ResourceType[iTypes.Length];
        for (int i = 0; i < iTypes.Length; i++)
            inTypes[i] = (ResourceType)(Enum.Parse(typeof(ResourceType), iTypes[i]));

        // parse input amounts
        byte[] inAmounts = new byte[iAmounts.Length];
        for (int i = 0; i < iAmounts.Length; i++)
            inAmounts[i] = byte.Parse(iAmounts[i]);

        return new BuildingConversion(type, position, inTypes, outTypes, inAmounts, outAmounts, speed);
    }

    // method for creating production buildings
    private BuildingProduction CreateProductionBuilding(BuildingType type, CPoint position)
    {
        // stats for current building
        List<string[]> building = this.buildings[type];

        string[] types = building[0][1].Split(',');
        string[] amounts = building[1][1].Split(',');
        int speed = int.Parse(building[2][1]);

        ResourceType[] rTypes = new ResourceType[types.Length];
        uint[] rAmounts = new uint[types.Length];
        for (int i = 0; i < types.Length; i++)
        {
            rTypes[i] = (ResourceType)(Enum.Parse(typeof(ResourceType), types[i]));
            rAmounts[i] = uint.Parse(amounts[i]);
        }

        return new BuildingProduction(type, position, rTypes, rAmounts, speed);
    }

    private List<string> LoadDirectory(string root)
    {
        List<string> filePaths = new List<string>();

        string[] files = Directory.GetFiles(root);
        foreach (string file in files)
            filePaths.Add(file);

        string[] directories = Directory.GetDirectories(root);
        foreach (string directory in directories)
            filePaths.AddRange(LoadDirectory(directory));

        return filePaths;
    }
}