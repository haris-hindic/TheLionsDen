import 'package:json_annotation/json_annotation.dart';
import 'package:the_lions_den_mobile/model/room_type/room_type_response.dart';
part 'facility_response.g.dart';

@JsonSerializable()
class FacilityResponse {
  int? facilityId;
  String? name;
  String? description;
  double? price;
  String? image;

  FacilityResponse();

  factory FacilityResponse.fromJson(Map<String, dynamic> json) =>
      _$FacilityResponseFromJson(json);

  /// Connect the generated [_$FacilityResponseToJson] function to the `toJson` method.
  Map<String, dynamic> toJson() => _$FacilityResponseToJson(this);
}
