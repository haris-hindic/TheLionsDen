import 'package:json_annotation/json_annotation.dart';
part 'room_image_response.g.dart';

@JsonSerializable()
class RoomImageResponse {
  int? roomImageId;
  int? roomTypeId;
  String? image;

  RoomImageResponse();

  factory RoomImageResponse.fromJson(Map<String, dynamic> json) =>
      _$RoomImageResponseFromJson(json);

  /// Connect the generated [_$RoomImageResponseToJson] function to the `toJson` method.
  Map<String, dynamic> toJson() => _$RoomImageResponseToJson(this);
}
