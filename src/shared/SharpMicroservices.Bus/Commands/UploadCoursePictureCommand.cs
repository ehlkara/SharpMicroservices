namespace SharpMicroservices.Bus.Commands;

public record UploadCoursePictureCommand(Guid CourseId, byte[] Picture);
