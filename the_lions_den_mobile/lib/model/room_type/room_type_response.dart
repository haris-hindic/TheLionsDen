import 'package:json_annotation/json_annotation.dart';
import 'package:the_lions_den_mobile/model/room_image/room_image_response.dart';
part 'room_type_response.g.dart';

@JsonSerializable()
class RoomTypeResponse {
  int? roomTypeId;
  String? name;
  String? description;
  String? rules;
  int? size;
  int? capacity;

  List<RoomImageResponse>? roomImages;

  RoomTypeResponse();

  factory RoomTypeResponse.fromJson(Map<String, dynamic> json) =>
      _$RoomTypeResponseFromJson(json);

  /// Connect the generated [_$RoomTypeResponseToJson] function to the `toJson` method.
  Map<String, dynamic> toJson() => _$RoomTypeResponseToJson(this);
}
