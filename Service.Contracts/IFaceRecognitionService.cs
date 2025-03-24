using Azure.AI.Vision.Face;

namespace Service.Contracts
{
    public interface IFaceRecognitionService
    {
        Task<IReadOnlyList<FaceDetectionResult>> DetectFacesFromUrlAsync(string imageUrl);
        Task<FaceVerificationResult> VerifyFacesFromUrlAsync(string imageUrl1, string imageUrl2);
    }
}
