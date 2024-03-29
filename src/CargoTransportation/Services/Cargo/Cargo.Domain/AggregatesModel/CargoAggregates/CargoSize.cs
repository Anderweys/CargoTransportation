﻿using CargoObject.Domain.SeedWork;
using CargoSizeReadModel = CargoObject.Domain.ReadModels.CargoAggregates.CargoSize;

namespace CargoObject.Domain.AggregatesModel.CargoAggregates;

public class CargoSize : ValueObject
{
    public float Length { get; private set; }
    public float Width { get; private set; }
    public float Height { get; private set; }
    public float Volume { get; private set; }

    public CargoSize()
    {
    }

    public CargoSize(float length, float width, float heigth)
    {
        Length = length;
        Width = width;
        Height = heigth;
        Volume = length * width * heigth;
    }

    public CargoSize ParseFromReadModel(CargoSizeReadModel readModel)
    {
        Length = readModel.Length;
        Width = readModel.Width;
        Height = readModel.Height;
        Volume = readModel.Volume;

        return this;
    }
}
