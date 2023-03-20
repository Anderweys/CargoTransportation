namespace WebMVC.Models.DTOs;

public record UserItemsDTO(string userId, IEnumerable<Item> Items);
