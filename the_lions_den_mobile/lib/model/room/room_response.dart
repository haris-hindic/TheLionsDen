import 'package:json_annotation/json_annotation.dart';
import 'package:the_lions_den_mobile/model/room_type/room_type_response.dart';
part 'room_response.g.dart';

@JsonSerializable()
class RoomResponse {
  int? roomId;
  double? price;
  String? name;
  String? amenities;
  String? roomTypeName;
  String? coverImage;
  RoomTypeResponse? roomType;

  RoomResponse();

  factory RoomResponse.fromJson(Map<String, dynamic> json) =>
      _$RoomResponseFromJson(json);

  /// Connect the generated [_$RoomResponseToJson] function to the `toJson` method.
  Map<String, dynamic> toJson() => _$RoomResponseToJson(this);
}
