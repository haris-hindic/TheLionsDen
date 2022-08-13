import 'package:json_annotation/json_annotation.dart';
import 'package:the_lions_den_mobile/model/room_type/room_type_response.dart';
part 'amenity_response.g.dart';

@JsonSerializable()
class AmenityResponse {
  int? amenityId;
  String? name;
  String? description;

  AmenityResponse();

  factory AmenityResponse.fromJson(Map<String, dynamic> json) =>
      _$AmenityResponseFromJson(json);

  /// Connect the generated [_$AmenityResponseToJson] function to the `toJson` method.
  Map<String, dynamic> toJson() => _$AmenityResponseToJson(this);
}
