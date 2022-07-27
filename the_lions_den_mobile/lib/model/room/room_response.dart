import 'package:json_annotation/json_annotation.dart';
part 'room_response.g.dart';

@JsonSerializable()
class RoomResponse {
  int? roomId;
  double? price;
  String? name;
  String? amenities;
  String? roomTypeName;
  String? coverImage;

  RoomResponse();

  factory RoomResponse.fromJson(Map<String, dynamic> json) =>
      _$RoomResponseFromJson(json);

  /// Connect the generated [_$RoomResponseToJson] function to the `toJson` method.
  Map<String, dynamic> toJson() => _$RoomResponseToJson(this);
}
