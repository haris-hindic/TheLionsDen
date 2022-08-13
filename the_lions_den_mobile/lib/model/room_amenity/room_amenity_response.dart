import 'package:json_annotation/json_annotation.dart';
import 'package:the_lions_den_mobile/model/amenity/amenity_response.dart';
import 'package:the_lions_den_mobile/model/room_type/room_type_response.dart';
part 'room_amenity_response.g.dart';

@JsonSerializable()
class RoomAmenityResponse {
  int? roomAmenityId;
  int? roomId;
  int? amenityId;
  String? amenityName;
  AmenityResponse? amenity;

  RoomAmenityResponse();

  factory RoomAmenityResponse.fromJson(Map<String, dynamic> json) =>
      _$RoomAmenityResponseFromJson(json);

  /// Connect the generated [_$RoomAmenityResponseToJson] function to the `toJson` method.
  Map<String, dynamic> toJson() => _$RoomAmenityResponseToJson(this);
}
