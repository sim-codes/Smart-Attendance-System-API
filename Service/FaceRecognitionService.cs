using Azure;
using Azure.AI.Vision.Face;
using Service.Contracts;

namespace Service
{
    public class FaceRecognitionService : IFaceRecognitionService
    {
        private readonly FaceClient _faceClient;
        private static readonly FaceDetectionModel _detectionModel = FaceDetectionModel.Detection03;
        private static readonly FaceRecognitionModel _recognitionModel = FaceRecognitionModel.Recognition04;

        // List of face attributes to detect
        private static readonly FaceAttributeType[] _faceAttributes = new[]
        {
            FaceAttributeType.Age,
            FaceAttributeType.HeadPose,
            FaceAttributeType.QualityForRecognition
        };

        public FaceRecognitionService(FaceClient faceClient) => _faceClient = faceClient;

        public async Task<IReadOnlyList<FaceDetectionResult>> DetectFacesFromUrlAsync(string imageUrl)
        {
            try
            {
                var response = await _faceClient.DetectAsync(
                    new Uri(imageUrl),
                    _detectionModel,
                    _recognitionModel,
                    returnFaceId: false);

                return response.Value;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error detecting faces: {ex.Message}");
            }
        }

        public async Task<FaceVerificationResult> VerifyFacesFromUrlAsync(string imageUrl1, string imageUrl2)
        {
            try
            {
                var faces1 = await DetectFacesFromUrlAsync(imageUrl1);
                var faces2 = await DetectFacesFromUrlAsync(imageUrl2);

                if (faces1.Count == 0 || faces2.Count == 0)
                    throw new Exception("No faces detected in one or both images");

                var faceId1 = faces1[0].FaceId.Value;
                var faceId2 = faces2[0].FaceId.Value;

                var response = await _faceClient.VerifyFaceToFaceAsync(faceId1, faceId2);
                return response.Value;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error verifying faces: {ex.Message}");
            }
        }
    }
}